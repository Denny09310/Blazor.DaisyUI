using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octokit;
using Octokit.Internal;
using System.Diagnostics;
using System.Net;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddScoped(ConfigureGitHubClient);
    services.AddScoped(sp => new HttpClient());
});

await builder.RunCommandLineApplicationAsync(args, app =>
{
    app.Command("generate", command =>
    {
        var name = command.Argument("name", "The name of the component to scaffold").IsRequired();

        command.OnExecuteAsync(async (cancellationToken) =>
        {
            using var scope = app.CreateScope();

            var githubClient = scope.ServiceProvider.GetRequiredService<GitHubClient>();
            
            var contents = await githubClient.Repository.Content.GetAllContentsByRef("Denny09310", "DaisyUI.Blazor", "Templates");

            var formattedName = name.Value.ToTitleCase() ?? throw new UnreachableException();
            var templateContent = contents.FirstOrDefault(c => c.Name.StartsWith(formattedName)) 
                ?? throw new Exception($"The selected template ({name.Value}). Does not exists");

            var proceed = Prompt.GetYesNo($"Template found. Do you want to proceed with the installation?", defaultAnswer: false);

            if (!proceed)
            {
                Console.Error.WriteLine("Operation cancelled by the user");
            }
            
            var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
            
            using var response = await httpClient.GetAsync(templateContent.DownloadUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            using var fileStream = File.OpenWrite(Path.Combine(@"C:\Temp", templateContent.Name));
            await stream.CopyToAsync(fileStream, cancellationToken);
        });
    });
});

static GitHubClient ConfigureGitHubClient(IServiceProvider sp)
{
    var proxy = new WebProxy("http://dkoja:Dk00.minecraft@tmws01.tech.ceia:8080");
    
    var connection = new Connection(
        new ProductHeaderValue("DaisyUI.Blazor.Tool"),
        new HttpClientAdapter(() => HttpMessageHandlerFactory.CreateDefault(proxy)));

    return new(connection);
}

static class StringExtensions
{
    public static string? ToTitleCase(this string? source)
    {
        if (source == null) return null;
        return char.ToUpper(source[0]) + source[1..];
    }
}
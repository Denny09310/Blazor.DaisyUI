using Blazor.DaisyUI.Tool.Contracts.Services;
using Blazor.DaisyUI.Tool.Extensions;
using Blazor.DaisyUI.Tool.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddGitHubClient();
    services.AddScoped(sp => new HttpClient());

    services.AddScoped<IHeaderParser, HeaderParser>();
    services.AddScoped<ITemplateDownloader, TemplateDownloader>();
});

await builder.RunCommandLineApplicationAsync(args, app =>
{
    app.Command("generate", command =>
    {
        command.Description = "scaffold a component template based on the current working directory";

        var name = command.Argument("name", "The name of the component to scaffold").IsRequired();
        var force = command.Option("--force", "Forces the donwload of the template, overwriting the existing one", CommandOptionType.SingleOrNoValue);

        command.OnExecuteAsync(async (cancellationToken) =>
        {
            using var scope = app.CreateScope();

            var downloader = scope.ServiceProvider.GetRequiredService<ITemplateDownloader>();
            await downloader.DownloadTemplateAsync(name.Value!, force.HasValue(), cancellationToken);
        });
    });
});

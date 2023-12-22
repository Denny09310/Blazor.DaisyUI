using Blazor.DaisyUI.Tool.Contracts.Services;
using Blazor.DaisyUI.Tool.Extensions;
using McMaster.Extensions.CommandLineUtils;
using Octokit;
using System.Diagnostics;

namespace Blazor.DaisyUI.Tool.Services;

internal class TemplateDownloader(IConsole console, IHeaderParser parser, IGitHubClient githubClient, HttpClient httpClient) : ITemplateDownloader
{
    private bool _recurse;

    public string DownloadDirectory => Path.Combine(Environment.CurrentDirectory, "Components", "UI");

    public async Task DownloadTemplateAsync(string name, CancellationToken cancellationToken = default)
    {
        var contents = await githubClient.Repository.Content.GetAllContents("Denny09310", "Blazor.DaisyUI", "Blazor.DaisyUI.Tool/Templates");

        var formattedName = name.ToTitleCase() ?? throw new UnreachableException();
        var templateContent = contents.FirstOrDefault(c => c.Name.StartsWith(formattedName))
            ?? throw new Exception($"The selected template ({name}). Does not exists");

        if (ConfirmationGranted()) return;

        console.WriteLine("Downloading {0} template", formattedName);

        using var response = await httpClient.GetAsync(templateContent.DownloadUrl, cancellationToken);
        response.EnsureSuccessStatusCode();

        console.WriteLine("{0} template downloaded", formattedName);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var headers = parser.ParseContent(content);

        var dependencies = headers
            .Where(header => header.Key == "depends-on")
            .Select(header => header.Value);

        if (dependencies.Any())
        {
            _recurse = true;

            console.WriteLine("Found template dependecies! Start downloading");
            console.DrawLine();

            foreach (var dependency in dependencies)
            {
                await DownloadTemplateAsync(dependency, cancellationToken);
            }
        }

        var indexOfHeaders = parser.GetIndexOfHeaders(content);
        var contentWithoutHeaders = content[indexOfHeaders..].Trim();

        var subPath = headers.FirstOrDefault(header => header.Key == "sub-path");
        var downloadDirectory = subPath != null ? Path.Combine(DownloadDirectory, subPath.Value) : DownloadDirectory;

        Directory.CreateDirectory(downloadDirectory);

        var extension = headers.FirstOrDefault(header => header.Key == "output") ?? new("output", ".cs");
        var dowloadPath = Path.Combine(downloadDirectory, $"{formattedName}{extension.Value}");

        await File.WriteAllTextAsync(dowloadPath, contentWithoutHeaders, cancellationToken);

        console.WriteLine("{0} written to disk", formattedName);
        console.DrawLine();
    }

    private bool ConfirmationGranted()
    {
        if (_recurse) return true;

        var proceed = Prompt.GetYesNo($"Template found. Do you want to proceed with the installation?", defaultAnswer: false);

        if (!proceed)
        {
            console.Error.WriteLine("Operation cancelled by the user");
        }

        return proceed;
    }
}
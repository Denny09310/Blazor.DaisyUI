using Blazor.DaisyUI.Tool.Contracts.Services;
using Blazor.DaisyUI.Tool.Extensions;
using Blazor.DaisyUI.Tool.Models;
using McMaster.Extensions.CommandLineUtils;
using Octokit;
using System.Diagnostics;

namespace Blazor.DaisyUI.Tool.Services;

internal class TemplateDownloader(IConsole console, IHeaderParser parser, IGitHubClient githubClient, HttpClient httpClient) : ITemplateDownloader
{
    private bool _alreadyGranted;
    private bool _force;

    public string DownloadDirectory => Path.Combine(Environment.CurrentDirectory, "Components", "UI");

    public async Task DownloadTemplateAsync(string name, bool force = false, CancellationToken cancellationToken = default)
    {
        _force = force;

        var contents = await githubClient.Repository.Content.GetAllContents("Denny09310", "Blazor.DaisyUI", "Blazor.DaisyUI.Tool/Templates");

        var formattedName = name.ToTitleCase() ?? throw new UnreachableException();
        var templateContent = contents.FirstOrDefault(c => c.Name.StartsWith(formattedName))
            ?? throw new Exception($"The selected template ({name}). Does not exists");

        if (!ConfirmationGranted()) return;

        console.Out.WriteLine("Downloading {0} template", formattedName);

        using var response = await httpClient.GetAsync(templateContent.DownloadUrl, cancellationToken);
        response.EnsureSuccessStatusCode();

        console.Out.WriteLine("{0} template downloaded", formattedName);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var headers = parser.ParseContent(content);

        var dependencies = headers
            .Where(header => header.Key == "depends-on")
            .Select(header => header.Value);

        await DownloadDependenciesAsync(dependencies, cancellationToken);
        await WriteTemplateAsync(formattedName, content, headers, cancellationToken);
    }

    private bool ConfirmationGranted()
    {
        if (_alreadyGranted) return true;

        var proceed = Prompt.GetYesNo($"Template found. Do you want to proceed with the installation?", defaultAnswer: false);

        if (!proceed)
        {
            console.Error.WriteLine("Operation cancelled by the user");
        }

        _alreadyGranted = true;
        return proceed;
    }

    private async Task DownloadDependenciesAsync(IEnumerable<string> dependencies, CancellationToken cancellationToken)
    {
        if (dependencies.Any())
        {
            console.Out.WriteLine("Found template dependecies! Start downloading");
            console.Out.DrawLine();

            foreach (var dependency in dependencies)
            {
                await DownloadTemplateAsync(dependency, _force, cancellationToken);
            }
        }
    }

    private bool TemplateExists(string path)
    {
        return !_force && File.Exists(path);
    }

    private async Task WriteTemplateAsync(string formattedName, string content, IEnumerable<Header> headers, CancellationToken cancellationToken)
    {
        var indexOfHeaders = parser.GetIndexOfHeaders(content);
        var contentWithoutHeaders = content[indexOfHeaders..].Trim();

        var subPath = headers.FirstOrDefault(header => header.Key == "sub-path");
        var downloadDirectory = subPath != null ? Path.Combine(DownloadDirectory, subPath.Value) : DownloadDirectory;

        Directory.CreateDirectory(downloadDirectory);

        var extension = headers.FirstOrDefault(header => header.Key == "output") ?? new("output", ".cs");
        var dowloadPath = Path.Combine(downloadDirectory, $"{formattedName}{extension.Value}");

        if (TemplateExists(dowloadPath))
        {
            var proceed = Prompt.GetYesNo($"The template {formattedName} already exists. Do you want to everwrite it?", defaultAnswer: false);

            if (!proceed)
            {
                console.Error.WriteLine("Operation cancelled by the user");
                return;
            }

            _force = true;
        }

        await File.WriteAllTextAsync(dowloadPath, contentWithoutHeaders, cancellationToken);

        console.Out.WriteLine("{0} written to disk", formattedName);
        console.Out.DrawLine();
    }
}
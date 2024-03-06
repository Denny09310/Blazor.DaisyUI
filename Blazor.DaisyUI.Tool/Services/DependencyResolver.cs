using Blazor.DaisyUI.Tool.Contracts.Services;
using Blazor.DaisyUI.Tool.Models;
using Octokit;
using Spectre.Console;
using System.Threading;

namespace Blazor.DaisyUI.Tool.Services;

internal class DependencyResolver(IDependencyStore store, ITemplateDownloader downloader, IHeaderParser parser) : IDependencyResolver
{
    public async Task ResolveDependenciesAsync(string name, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        name = name.ToTitleCase();

        var (content, headers) = await FetchRepositoryContentAndHeaders(name, cancellationToken);

        var dependencies = headers
            .Where(header => header.Key == "depends-on")
            .Select(header => header.Value);

        if (dependencies.Any())
        {
            foreach (var dependency in dependencies)
            {
                await ResolveDependenciesAsync(dependency, cancellationToken);
            }
        }

        AnsiConsole.MarkupLineInterpolated($"Dependency found: {name}");

        store.AddDependency(name, content, headers);
    }

    public async Task<bool> HasDependenciesAsync(string downloadUrl, CancellationToken cancellationToken = default)
    {
        var content = await downloader.GetTemplateAsync(downloadUrl, cancellationToken);
        var headers = parser.ParseContent(content);

        var dependencies = headers
            .Where(header => header.Key == "depends-on")
            .Select(header => header.Value);

        return dependencies.Any();
    }

    private async Task<Dependency> FetchRepositoryContentAndHeaders(string name, CancellationToken cancellationToken)
    {
        var repository = await downloader.GetFromRepositoryAsync(name, cancellationToken);
        var content = await downloader.GetTemplateAsync(repository.DownloadUrl, cancellationToken);
        var headers = parser.ParseContent(content);

        return new(content, headers);
    }

    private record Dependency(string Content, IEnumerable<Header> Headers);
}
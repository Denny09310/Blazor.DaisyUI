using Blazor.DaisyUI.Tool.Contracts.Services;
using Spectre.Console;

namespace Blazor.DaisyUI.Tool.Services;

internal class DependencyResolver(IDependencyStore store, ITemplateDownloader downloader, IHeaderParser parser) : IDependencyResolver
{
    public async Task ResolveDependenciesAsync(string name, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        name = name.ToTitleCase();

        var repository = await downloader.GetFromRepositoryAsync(name, cancellationToken);
        var content = await downloader.GetTemplateAsync(repository, cancellationToken);

        var headers = parser.ParseContent(content);

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
}
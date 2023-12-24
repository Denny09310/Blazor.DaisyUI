using Blazor.DaisyUI.Tool.Contracts.Services;
using McMaster.Extensions.CommandLineUtils;

namespace Blazor.DaisyUI.Tool.Services;

internal class TemplateScaffolder(IDependencyStore store, ITemplateDownloader downloader, IHeaderParser parser) : ITemplateScaffolder
{
    public string ScaffoldDirectory => Path.Combine(Environment.CurrentDirectory, "Components", "UI");

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

        store.AddDependency(name, content, headers);
    }

    public async Task ScaffoldTemplatesAsync(bool force, CancellationToken cancellationToken = default)
    {
        foreach (var (key, value) in store.Dependencies)
        {
            var subPath = value.Headers.FirstOrDefault(header => header.Key == "sub-path");
            var downloadDirectory = subPath != null ? Path.Combine(ScaffoldDirectory, subPath.Value) : ScaffoldDirectory;

            Directory.CreateDirectory(downloadDirectory);

            var extension = value.Headers.FirstOrDefault(header => header.Key == "output") ?? new("output", ".cs");
            var dowloadPath = Path.Combine(downloadDirectory, $"{key}{extension.Value}");

            if (!force && File.Exists(dowloadPath))
            {
                var proceed = Prompt.GetYesNo("This component has already been installed. Do you want to overwrite it?", defaultAnswer: false);

                if (!proceed) continue;
            }

            await File.WriteAllTextAsync(dowloadPath, value.Content, cancellationToken);
        }

        Console.WriteLine("Insallation completed");
    }
}
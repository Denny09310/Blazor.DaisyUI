using Blazor.DaisyUI.Tool.Contracts.Services;
using Octokit;
using System.Globalization;

namespace Blazor.DaisyUI.Tool.Services;

internal class TemplateDownloader(IGitHubClient github, HttpClient client) : ITemplateDownloader
{
#if DEBUG
        const string BRANCH = "develop";
#else
        const string BRANCH = "master";
#endif

    private const string COMPONENT_SUFFIX = "Component";

    public async Task<RepositoryContent> GetFromRepositoryAsync(string name, CancellationToken cancellationToken = default)
    {
        name = name.Replace(COMPONENT_SUFFIX, string.Empty);

        var repository = await github.Repository.Content
            .GetAllContentsByRef("Denny09310", "Blazor.DaisyUI", "Blazor.DaisyUI.Tool/Templates", BRANCH);

        return repository.FirstOrDefault(x => x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
            ?? throw new Exception($"The template {name} does not exists. Aborting");
    }

    public Task<string> GetTemplateAsync(string downloadUrl, CancellationToken cancellationToken = default)
    {
        return client.GetStringAsync(downloadUrl, cancellationToken);
    }
}
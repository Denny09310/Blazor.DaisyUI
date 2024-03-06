using Octokit;

namespace Blazor.DaisyUI.Tool.Contracts.Services
{
    internal interface ITemplateDownloader
    {
        Task<RepositoryContent> GetFromRepositoryAsync(string name, CancellationToken cancellationToken = default);
        Task<string> GetTemplateAsync(string downloadUrl, CancellationToken cancellationToken = default);
    }
}
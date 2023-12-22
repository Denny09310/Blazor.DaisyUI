namespace Blazor.DaisyUI.Tool.Contracts.Services
{
    internal interface ITemplateDownloader
    {
        string DownloadDirectory { get; }

        Task DownloadTemplateAsync(string name, CancellationToken cancellationToken = default);
    }
}
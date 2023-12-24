namespace Blazor.DaisyUI.Tool.Contracts.Services
{
    internal interface ITemplateScaffolder
    {
        string ScaffoldDirectory { get; }

        Task ResolveDependenciesAsync(string name, CancellationToken cancellationToken = default);
        Task ScaffoldTemplatesAsync(bool force, CancellationToken cancellationToken = default);
    }
}
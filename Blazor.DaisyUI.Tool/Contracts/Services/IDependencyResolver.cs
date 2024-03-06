namespace Blazor.DaisyUI.Tool.Contracts.Services
{
    internal interface IDependencyResolver
    {
        Task<bool> HasDependenciesAsync(string downloadUrl, CancellationToken cancellationToken = default);
        Task ResolveDependenciesAsync(string name, CancellationToken cancellationToken = default);
    }
}
namespace Blazor.DaisyUI.Tool.Contracts.Services
{
    internal interface IDependencyResolver
    {
        Task ResolveDependenciesAsync(string name, CancellationToken cancellationToken = default);
    }
}
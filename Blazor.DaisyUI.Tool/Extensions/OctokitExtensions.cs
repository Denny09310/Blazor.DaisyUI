using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace Blazor.DaisyUI.Tool.Extensions;

internal static class OctokitExtensions
{
    public static IServiceCollection AddGitHubClient(this IServiceCollection services)
    {
        return services.AddScoped<IGitHubClient>(sp =>
        {
            return new GitHubClient(new ProductHeaderValue("DaisyUI.Blazor.Tool"));
        });
    }
}
using Microsoft.Extensions.DependencyInjection;
using Octokit;
using Octokit.Internal;
using System.Net;

namespace Blazor.DaisyUI.Tool.Extensions;

internal static class OctokitExtensions
{
    public static IServiceCollection AddGitHubClient(this IServiceCollection services)
    {
        return services.AddScoped<IGitHubClient>(sp =>
        {
            var proxy = new WebProxy("http://dkoja:Dk00.minecraft@tmws01.tech.ceia:8080");

            var connection = new Connection(
                new ProductHeaderValue("DaisyUI.Blazor.Tool"),
                new HttpClientAdapter(() => HttpMessageHandlerFactory.CreateDefault(proxy)));

            return new GitHubClient(connection);
        });
    }
}
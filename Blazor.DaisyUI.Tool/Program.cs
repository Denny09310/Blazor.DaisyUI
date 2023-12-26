using Blazor.DaisyUI.Tool.Commands;
using Blazor.DaisyUI.Tool.Contracts.Services;
using Blazor.DaisyUI.Tool.Extensions;
using Blazor.DaisyUI.Tool.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddGitHubClient();
services.AddScoped(sp => new HttpClient());

services.AddScoped<IHeaderParser, HeaderParser>();
services.AddScoped<IDependencyStore, DependencyStore>();
services.AddScoped<ITemplateDownloader, TemplateDownloader>();
services.AddScoped<IDependencyResolver, DependencyResolver>();

var registrar = new DependencyInjectionRegistrar(services);
var app = new CommandApp(registrar);

app.Configure(config =>
{
    config.SetApplicationName("daisyui")
          .SetApplicationVersion("0.0.4-beta");

    config.AddCommand<GenerateCommand>("generate")
          .WithDescription("Scaffold a component template along with needed dependencies")
          .WithExample(["generate", "button"]);
});

await app.RunAsync(args);
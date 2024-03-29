﻿using Blazor.DaisyUI.Tool.Contracts.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Blazor.DaisyUI.Tool.Commands;

internal sealed class GenerateCommand(IDependencyStore store, IDependencyResolver resolver) : AsyncCommand<GenerateCommand.Settings>
{
    public static string ScaffoldDirectory => Path.Combine(Environment.CurrentDirectory, "Components", "UI");

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        await AnsiConsole.Status().StartAsync("Resolving dependencies...",
            (context) => resolver.ResolveDependenciesAsync(settings.Name));

        await AnsiConsole.Progress().StartAsync(async (context) =>
        {
            var task = context.AddTask("Downloading templates");
            var tick = 100 / store.Dependencies.Count;

            foreach (var (key, value) in store.Dependencies)
            {
                try
                {
                    var subPath = value.Headers.FirstOrDefault(header => header.Key == "sub-path");
                    var downloadDirectory = subPath != null ? Path.Combine(ScaffoldDirectory, subPath.Value) : ScaffoldDirectory;

                    Directory.CreateDirectory(downloadDirectory);

                    var extension = value.Headers.FirstOrDefault(header => header.Key == "output") ?? new("output", ".cs");
                    var dowloadPath = Path.Combine(downloadDirectory, $"{key}{extension.Value}");

                    if (!settings.Force && File.Exists(dowloadPath))
                    {
                        // TODO: Can't run prompt and progress bar, for now if it exists don't touch it
                        // var proceed = AnsiConsole.Confirm("This component has already been installed. Do you want to overwrite it?", defaultValue: false);

                        if (true) continue;
                    }

                    await File.WriteAllTextAsync(dowloadPath, value.Content);
                }
                finally
                {
                    task.Increment(tick);
                }
            }
        });

        AnsiConsole.MarkupLine("[green]Installation completed[/]");

        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        [CommandOption("--force")]
        [Description("Tells the program if the overwrite confirmation is needed")]
        public bool Force { get; init; }

        [CommandArgument(0, "[name]")]
        [Description("Name of the template to scaffold inside the project")]
        public string Name { get; init; } = null!;

        public override ValidationResult Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return ValidationResult.Error("The name of the component to scaffold must be specified");
            }

            return ValidationResult.Success();
        }
    }
}
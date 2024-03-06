﻿using Blazor.DaisyUI.Tool.Contracts.Services;
using Octokit;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Blazor.DaisyUI.Tool.Commands;

internal sealed class ListCommand(IGitHubClient github, IDependencyResolver resolver) : AsyncCommand<ListCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var branch = settings.PreRelease ? "develop" : "master";

        var templates = await AnsiConsole.Status().StartAsync($"Fetching templates from {branch} branch", (context) =>
            github.Repository.Content.GetAllContentsByRef("Denny09310", "Blazor.DaisyUI", "Blazor.DaisyUI.Tool/Templates", branch));

        var table = new Table { Border = TableBorder.SimpleHeavy };
        await AnsiConsole.Live(table).StartAsync(async (context) =>
        {
            table.AddColumns("Name", "Description", "Dependencies", "Tags");
            context.Refresh();

            foreach (var template in templates)
            {
                var name = Path.GetFileNameWithoutExtension(template.Name);
                var hasDependencies = await resolver.HasDependenciesAsync(template.DownloadUrl);

                table.AddRow(name, string.Empty, hasDependencies.ToString(), string.Empty);
                context.Refresh();
            }

            return table;
        });

        return 0;
    }

    public sealed class Settings : CommandSettings
    {
        [CommandOption("--prerelease")]
        [Description("Get the component list that is not already published")]
        public bool PreRelease { get; set; }
    }
}
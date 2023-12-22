﻿using McMaster.Extensions.CommandLineUtils;

namespace Blazor.DaisyUI.Tool.Extensions;

static class ConsoleExtensions
{
    public static void DrawLine(this TextWriter writer) => writer.WriteLine(string.Empty.PadLeft(Console.WindowWidth, '-'));
    public static void DrawLine(this IConsole console) => console.WriteLine(string.Empty.PadLeft(Console.WindowWidth, '-'));
}
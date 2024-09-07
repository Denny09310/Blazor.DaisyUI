﻿using Blazor.DaisyUI.Enums;

namespace Blazor.DaisyUI.Helpers;

public class AlertIcon
{
    private const string Error = "<svg xmlns=\"http://www.w3.org/2000/svg\" style=\"width: 1.5rem; height: 1.5rem; flex-shrink: 0; stroke: ;\" fill=\"none\" viewBox=\"0 0 24 24\"><path stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z\" /></svg>";
    private const string Info = "<svg xmlns=\"http://www.w3.org/2000/svg\" style=\"width: 1.5rem; height: 1.5rem; flex-shrink: 0; stroke: ;\" fill=\"none\" viewBox=\"0 0 24 24\"><path stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z\"></path></svg>";
    private const string Success = "<svg xmlns=\"http://www.w3.org/2000/svg\" style=\"width: 1.5rem; height: 1.5rem; flex-shrink: 0; stroke: ;\" fill=\"none\" viewBox=\"0 0 24 24\"><path stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z\" /></svg>";
    private const string Warning = "<svg xmlns=\"http://www.w3.org/2000/svg\" style=\"width: 1.5rem; height: 1.5rem; flex-shrink: 0; stroke: ;\" fill=\"none\" viewBox=\"0 0 24 24\"><path stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\" d=\"M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z\" /></svg>";

    public static string GetAlertIcon(Variant variant, bool currentColor)
    {
        var color = variant.ToString()[..2].ToLower();
        var stroke = currentColor ? "currentColor;" : $"var(--fallback-in,oklch(var(--{color})/1));";
        var icon = variant switch
        {
            Variant.Info => Info,
            Variant.Success => Success,
            Variant.Warning => Warning,
            Variant.Error => Error,
            _ => throw new NotImplementedException()
        };

        var placeholder = "stroke:";
        var indexOfStart = icon.IndexOf(placeholder);
        return icon.Insert(indexOfStart + placeholder.Length, stroke);
    }
}

public class AccordionIcon
{
    public const string Arrow = "collapse-arrow";
    public const string Plus = "collapse-plus";
}
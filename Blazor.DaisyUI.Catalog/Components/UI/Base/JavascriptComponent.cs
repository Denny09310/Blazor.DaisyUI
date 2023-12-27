﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.DaisyUI;

public class JavascriptComponent : ComponentBase
{
    [Inject]
    public IJSRuntime JsRuntime { get; set; } = default!;

    protected bool IsJsRuntimeAvailable { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        IsJsRuntimeAvailable = true;
        base.OnAfterRender(firstRender);
    }
}
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazor.DaisyUI;

public class Polymorph : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string As { get; set; } = "span";

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        builder.OpenElement(0, As);

        if (AdditionalAttributes != null)
        {
            foreach (var attribute in AdditionalAttributes)
            {
                if (attribute.Value == null) continue;
                builder.AddAttribute(1, attribute.Key, attribute.Value);
            }
        }

        if (!string.IsNullOrEmpty(Class))
        {
            builder.AddAttribute(2, "class", Class);
        }

        if (!string.IsNullOrEmpty(Style))
        {
            builder.AddAttribute(3, "style", Style);
        }

        builder.AddContent(6, ChildContent);
        builder.CloseComponent();
    }
}
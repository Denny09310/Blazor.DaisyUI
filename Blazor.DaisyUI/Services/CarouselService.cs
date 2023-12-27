using Microsoft.JSInterop;

namespace Blazor.DaisyUI.Services;

public interface ICarouselService
{
    ValueTask ScrollSlideIntoViewAsync(string slideId);
}

public class CarouselService(IJSRuntime jsRuntime) : ICarouselService
{
    private IJSObjectReference? _module;

    public async ValueTask ScrollSlideIntoViewAsync(string slideId)
    {
        var module = await ImportModuleAsync();
        await module.InvokeVoidAsync("scrollSlideIntoView", slideId);
    }

    private async ValueTask<IJSObjectReference> ImportModuleAsync()
    {
        return _module ??= await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Blazor.DaisyUI/carousel.js");
    }
}
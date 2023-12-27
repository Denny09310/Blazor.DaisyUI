using Blazor.DaisyUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.DaisyUI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDaisyUi(this IServiceCollection services) => services
        .AddScoped<ICarouselService, CarouselService>();
}
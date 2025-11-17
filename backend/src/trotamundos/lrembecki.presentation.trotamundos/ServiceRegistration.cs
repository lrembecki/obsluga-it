using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Services;
using lrembecki.core.trotamundos.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace lrembecki.presentation.trotamundos;

public static class ServiceRegistration
{
    public static void AddTrotamundos(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAdvantageService, AdvantageService>();
        builder.Services.AddScoped<IHighlightService, HighlightService>();
        builder.Services.AddScoped<ILoyalityProgramService, LoyalityProgramService>();
        builder.Services.AddScoped<ITripService, TripService>();
    }

    public static void MapTrotamundos(this WebApplication app)
    {
        var group = app.MapGroup("/api/trotamundos")
            .WithTags("Trotamundos")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapCrud<IAdvantageService, AdvantageDto, AdvantageVM>("advantages");
        group.MapCrud<IHighlightService, HighlightDto, HighlightVM>("highlights");
        group.MapCrud<ILoyalityProgramService, LoyalityProgramDto, LoyalityProgramVM>("loyality-programs");
        group.MapCrud<ITripService, TripDto, TripVM>("trips");
    }
}

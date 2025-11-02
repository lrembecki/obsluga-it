using lrembecki.core.trotamundos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.presentation.trotamundos;

public static class ServiceRegistration
{
    public static void AddTrotamundos(this WebApplicationBuilder builder)
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

        group.MapAdvantages();
        group.MapHighlights();
        group.MapLoyalityPrograms();
        group.MapTrips();
    }
}

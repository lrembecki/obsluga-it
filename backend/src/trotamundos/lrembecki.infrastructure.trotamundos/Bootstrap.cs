using lrembecki.core.trotamundos.Advantages;
using lrembecki.core.trotamundos.Files;
using lrembecki.core.trotamundos.Highlights;
using lrembecki.core.trotamundos.LoyalityPrograms;
using lrembecki.core.trotamundos.Settings;
using lrembecki.core.trotamundos.Trips;
using lrembecki.infrastructure.shared;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class BootstrapTrotamundos
{
    public static IServiceCollection AddTrotamundos(this IServiceCollection services)
    {
        services.AddScoped<IAboutUsService, AboutUsService>();
        services.AddScoped<IAdvantageService, AdvantageService>();
        services.AddScoped<IHighlightService, HighlightService>();
        services.AddScoped<ILoyalityProgramService, LoyalityProgramService>();
        services.AddScoped<ITripService, TripService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }

    public static WebApplication MapTrotamundos(this WebApplication app)
    {
        var group = app.MapGroup("/api/trotamundos")
            .WithTags("Trotamundos")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapCrud<IAboutUsService, AboutUsDto, AboutUsVM>("about-us");
        group.MapCrud<IAdvantageService, AdvantageDto, AdvantageVM>("advantages");
        group.MapCrud<IHighlightService, HighlightDto, HighlightVM>("highlights");
        group.MapCrud<ILoyalityProgramService, LoyalityProgramDto, LoyalityProgramVM>("loyality-programs");
        group.MapCrud<ITripService, TripDto, TripVM>("trips");
        group.MapCrud<IFileService, FileDto, FileVM>("files");

        return app;
    }

    public static ModelBuilder ApplyConfigurationFromTrotamundos(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BootstrapTrotamundos).Assembly);
        return modelBuilder;
    }
}

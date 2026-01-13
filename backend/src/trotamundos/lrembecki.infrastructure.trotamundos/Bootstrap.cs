using lrembecki.core;
using lrembecki.core.Services;
using lrembecki.core.trotamundos.Advantages;
using lrembecki.core.trotamundos.Files;
using lrembecki.core.trotamundos.Highlights;
using lrembecki.core.trotamundos.LoyalityPrograms;
using lrembecki.core.trotamundos.Pages.AboutUs;
using lrembecki.core.trotamundos.Pages.HowItWorks;
using lrembecki.core.trotamundos.Trips;
using lrembecki.infrastructure.shared;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class BootstrapTrotamundos
{
    public static IServiceCollection AddTrotamundos(this IServiceCollection services)
    {
        services.AddScoped<IAdvantageService, AdvantageService>();
        services.AddScoped<IHighlightService, HighlightService>();
        services.AddScoped<ILoyalityProgramService, LoyalityProgramService>();
        services.AddScoped<ITripService, TripService>();
        services.AddScoped<IFileService, FileService>();

        services.AddScoped<IHandler<AboutUsGetAllRequest, AboutUsVM>, AboutUsGetAllRequest.Handler>();
        services.AddScoped<IHandler<AboutUsCreateOrUpdateRequest, AboutUsVM>, AboutUsCreateOrUpdateRequest.Handler>();

        services.AddScoped<IHandler<HowItWorksGetAllRequest, HowItWorksVM>, HowItWorksGetAllRequest.Handler>();
        services.AddScoped<IHandler<HowItWorksCreateOrUpdateRequest, HowItWorksVM>, HowItWorksCreateOrUpdateRequest.Handler>();

        return services;
    }

    public static WebApplication MapTrotamundos(this WebApplication app)
    {
        var group = app.MapGroup("/api/trotamundos")
            .WithTags("Trotamundos")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapCrud<IAdvantageService, AdvantageDto, AdvantageVM>("advantages");
        group.MapCrud<IHighlightService, HighlightDto, HighlightVM>("highlights");
        group.MapCrud<ILoyalityProgramService, LoyalityProgramDto, LoyalityProgramVM>("loyality-programs");
        group.MapCrud<ITripService, TripDto, TripVM>("trips");
        group.MapCrud<IFileService, FileDto, FileVM>("files");

        MapPages(group);

        return app;
    }
    private static void MapPages(RouteGroupBuilder group)
    {
        var pagesGroup = group.MapGroup("pages")
            .WithTags("Trotamundos Pages");

        MapPagesAboutUs(pagesGroup);
        MapPagesHowItWorks(pagesGroup);
    }
    private static void MapPagesAboutUs(RouteGroupBuilder pagesGroup)
    {
        pagesGroup
            .MapGet("about-us", (ISender sender, CancellationToken ct) =>
                sender.Send(new AboutUsGetAllRequest(), ct)
                .ToServiceCallResultAsync())
            .RequireAuthorization(e => e.RequireRole("Trotamundos.Pages.AboutUs"));

        pagesGroup
            .MapPut("about-us", (AboutUsDto model, ISender sender, CancellationToken ct) =>
                sender.Send(new AboutUsCreateOrUpdateRequest(model), ct)
                .ToServiceCallResultAsync())
            .RequireAuthorization(e => e.RequireRole("Trotamundos.Pages.AboutUs"));
    }

    private static void MapPagesHowItWorks(RouteGroupBuilder pagesGroup)
    {
        pagesGroup
            .MapGet("how-it-works", (ISender sender, CancellationToken ct) =>
                sender.Send(new HowItWorksGetAllRequest(), ct)
                .ToServiceCallResultAsync())
            .RequireAuthorization(e => e.RequireRole("Trotamundos.Pages.HowItWorks"));

        pagesGroup
            .MapPut("how-it-works", (HowItWorksDto model, ISender sender, CancellationToken ct) =>
                sender.Send(new HowItWorksCreateOrUpdateRequest(model), ct)
                .ToServiceCallResultAsync())
            .RequireAuthorization(e => e.RequireRole("Trotamundos.Pages.HowItWorks"));
    }

    public static ModelBuilder ApplyConfigurationFromTrotamundos(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BootstrapTrotamundos).Assembly);
        return modelBuilder;
    }
}

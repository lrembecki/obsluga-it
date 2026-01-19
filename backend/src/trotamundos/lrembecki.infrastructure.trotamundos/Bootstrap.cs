using lrembecki.core.Services;
using lrembecki.core.trotamundos.Advantages;
using lrembecki.core.trotamundos.Files;
using lrembecki.core.trotamundos.Highlights;
using lrembecki.core.trotamundos.LoyalityPrograms;
using lrembecki.core.trotamundos.Pages.AboutUs;
using lrembecki.core.trotamundos.Pages.Home;
using lrembecki.core.trotamundos.Pages.HowItWorks;
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
            .RequireAuthorization("InternalJwtPolicy")
            .MapCrud<IAdvantageService, AdvantageDto, AdvantageVM>("advantages", _ => _.RequireAuthorization(e => e.RequireRole("Trotamundos.Adventages")))
            .MapCrud<IHighlightService, HighlightDto, HighlightVM>("highlights", _ => _.RequireAuthorization(e => e.RequireRole("Trotamundos.Highlights")))
            .MapCrud<ILoyalityProgramService, LoyalityProgramDto, LoyalityProgramVM>("loyality-programs", _ => _.RequireAuthorization(e => e.RequireRole("Trotamundos.LoyalityPrograms")))
            .MapCrud<ITripService, TripDto, TripVM>("trips", _ => _.RequireAuthorization(e => e.RequireRole("Trotamundos.Trips")))
            .MapCrud<IFileService, FileDto, FileVM>("files", _ => _.RequireAuthorization(e => e.RequireRole("Trotamundos.Files")))
            ;

        var pagesGroup = group.MapGroup("pages")
            .WithTags("Trotamundos Pages")
            ;

        pagesGroup.MapGroup("about-us")
            .RequireAuthorization(e => e.RequireRole("Trotamundos.Pages.AboutUs"))
            .MapGetRequest(AboutUsGetAllRequest.Delegate)
            .MapPutRequest(AboutUsCreateOrUpdateRequest.Delegate)
            ;

        pagesGroup.MapGroup("how-it-works")
            .RequireAuthorization(e => e.RequireRole("Trotamundos.Pages.HowItWorks"))
            .MapGetRequest(HowItWorksGetAllRequest.Delegate)
            .MapPutRequest(HowItWorksCreateOrUpdateRequest.Delegate)
            ;

        pagesGroup.MapGroup("home")
            .RequireAuthorization(e => e.RequireRole("Trotamundos.Pages.Home"))
            .MapGetRequest(HomeGetAllRequest.Delegate)
            .MapPutRequest(HomeCreateOrUpdateRequest.Delegate)
            ;

        return app;
    }

    public static ModelBuilder ApplyConfigurationFromTrotamundos(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BootstrapTrotamundos).Assembly);
        return modelBuilder;
    }
}

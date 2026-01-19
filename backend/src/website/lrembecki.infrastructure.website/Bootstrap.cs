using lrembecki.core;
using lrembecki.core.Services;
using lrembecki.core.website.Settings;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class BootstrapWebsite
{
    public static IServiceCollection AddWebsite(this IServiceCollection services)
    {
        services.AddScoped<IHandler<SettingsSyncCommand, SettingsVM>, SettingsSyncCommand.SettingsSyncCommandHandler>();

        return services;
    }

    public static WebApplication MapWebsite(this WebApplication app)
    {
        var group = app.MapGroup("/api/website")
            .WithTags("Website")
            .RequireAuthorization("InternalJwtPolicy");
        
        group.MapPost("/settings", async (ISender sender, SettingsDto model) => sender.Send(new SettingsSyncCommand(model)).ToServiceCallResult())
            .WithName("SyncSettings")
            .RequireAuthorization(e => e.RequireRole("WebSite.Settings"));

        return app;
    }

    public static ModelBuilder ApplyConfigurationFromWebsite(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BootstrapWebsite).Assembly);
        return modelBuilder;
    }
}

using lrembecki.core.settings.Dtos;
using lrembecki.core.settings.Services;
using lrembecki.core.settings.ViewModels;
using lrembecki.infrastructure.shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class BootstrapSettings
{
    public static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();

        return services;
    }

    public static WebApplication MapSettings(this WebApplication app)
    {
        var group = app.MapGroup("/api/settings")
            .WithTags("Settings")
            .RequireAuthorization("InternalJwtPolicy");


        group.MapCrud<IContactService, ContactDto, ContactVM>("contacts");
        group.MapCrud<IEmailTemplateService, EmailTemplateDto, EmailTemplateVM>("email-templates");
        group.MapCrud<IEmailService, EmailDto, EmailWithPasswordVM>("emails");
        group.MapCrud<IFtpService, FtpDto, FtpVM>("ftps");

        return app;
    }

    public static ModelBuilder ApplyConfigurationFromSettings(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BootstrapSettings).Assembly);
        return modelBuilder;
    }
}

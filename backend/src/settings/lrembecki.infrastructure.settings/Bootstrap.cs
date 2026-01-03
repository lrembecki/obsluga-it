using lrembecki.core.settings.Address;
using lrembecki.core.settings.Companies;
using lrembecki.core.settings.Contacts;
using lrembecki.core.settings.Emails;
using lrembecki.core.settings.EmailTemplates;
using lrembecki.core.settings.Ftps;
using lrembecki.infrastructure.shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class BootstrapSettings
{
    public static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICompanyService, CompanyService>();
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

        group.MapCrud<ICompanyService, CompanyDto, CompanyVM>("companies");
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

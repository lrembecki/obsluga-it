using lrembecki.core.settings.Dtos;
using lrembecki.core.settings.Services;
using lrembecki.core.settings.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.presentation.settings;

public static class ServiceRegistration
{
    public static void AddSettings(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IContactService, ContactService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
    }

    public static void MapSettings(this WebApplication app)
    {

        var group = app.MapGroup("/api/settings")
            .WithTags("Settings")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapCrud<IContactService, ContactDto, ContactVM>("contacts");
        group.MapCrud<IEmailService, EmailDto, EmailVM>("emails");
    }
}

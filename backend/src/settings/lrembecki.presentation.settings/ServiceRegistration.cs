using lrembecki.core.Services;
using lrembecki.core.settings.Entities;
using lrembecki.presentation.settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.presentation.settings;

public static class ServiceRegistration
{
    public static void AddSettings(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IContactService, ContactService>();
    }

    public static void MapSettings(this WebApplication app)
    {

        var group = app.MapGroup("/api/settings")
            .WithTags("Settings")
            .RequireAuthorization("InternalJwtPolicy");

        group.MapContacts();
    }
}

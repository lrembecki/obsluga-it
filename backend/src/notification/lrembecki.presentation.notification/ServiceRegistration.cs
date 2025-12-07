using lrembecki.core.notification.EmailNotifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace lrembecki.presentation.notification;

public static class ServiceRegistration
{
    public static void AddNotification(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();
    }

    public static void MapNotification(this WebApplication app)
    {
        var group = app.MapGroup("/api/notifications")
            .WithTags("Notifications")
            .RequireAuthorization("InternalJwtPolicy");

        var endpoints = group.MapCrud<IEmailNotificationService, EmailNotificationDto, EmailNotificationVM>("email-notifications");
    }
}

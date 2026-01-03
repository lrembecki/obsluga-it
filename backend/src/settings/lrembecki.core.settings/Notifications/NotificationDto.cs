namespace lrembecki.core.settings.Notifications;

public record NotificationDto
{
    public EmailNotificationDto Email { get; init; } = null!;
}

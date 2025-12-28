namespace lrembecki.core.settings.Dtos;

public record NotificationDto
{
    public EmailNotificationDto Email { get; init; } = null!;
}

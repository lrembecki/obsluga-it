namespace lrembecki.core.notification.EmailNotifications;

public record EmailNotificationDto
{
    public Guid Id { get; init; }
    public DateTime? Sent { get; init; }
    public string Recipients { get; init; } = null!;
    public string Subject { get; init; } = null!;
    public string Body { get; init; } = null!;
}

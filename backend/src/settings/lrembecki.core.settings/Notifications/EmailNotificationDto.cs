namespace lrembecki.core.settings.Notifications;

public record EmailNotificationDto
{
    public Guid EmailId { get; init; }
    public Guid EmailTemplateId { get; init; }
}

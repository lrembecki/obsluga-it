namespace lrembecki.core.settings.Dtos;

public record EmailNotificationDto
{
    public Guid EmailId { get; init; }
    public Guid EmailTemplateId { get; init; }
}

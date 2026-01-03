using lrembecki.core.settings.Notifications;

namespace lrembecki.core.forms.FormDefinitions;

public record FormDefinitionDto
{
    public string Name { get; init; } = string.Empty;
    public Guid? NotificationId { get; init; }
    public NotificationDto? Notification { get; init; } = null!;
    public List<FormFieldDefinitionDto> Fields { get; init; } = [];
    public List<FormDefinitionEmailNotificationFieldMappingDto> EmailNotificationFieldMapping { get; init; } = [];
}

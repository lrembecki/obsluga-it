namespace lrembecki.core.forms.FormDefinitions;

public record FormDefinitionEmailNotificationFieldMappingDto
{
    public string EmailTemplateFieldName { get; init; } = string.Empty;
    public string FormDefinitionFieldName { get; init; } = string.Empty;
}

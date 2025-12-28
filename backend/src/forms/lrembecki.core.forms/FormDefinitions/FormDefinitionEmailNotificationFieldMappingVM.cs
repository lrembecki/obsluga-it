namespace lrembecki.core.forms.FormDefinitions;

public record FormDefinitionEmailNotificationFieldMappingVM(string EmailTemplateFieldName, string FormDefinitionFieldName)
{
    internal static FormDefinitionEmailNotificationFieldMappingVM Map(FormDefinitionEmailNotificationFieldMappingEntity mapping)
    {
        if (mapping == null) return null!;

        return new FormDefinitionEmailNotificationFieldMappingVM(mapping.EmailTemplateFieldName, mapping.FormDefinitionFieldName);
    }
}
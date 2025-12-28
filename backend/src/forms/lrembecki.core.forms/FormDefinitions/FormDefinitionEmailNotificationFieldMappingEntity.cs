namespace lrembecki.core.forms.FormDefinitions;

internal class FormDefinitionEmailNotificationFieldMappingEntity
{
    public Guid FormDefinitionId { get; private set; }
    public string FormDefinitionFieldName { get; private set; } = string.Empty;
    public string EmailTemplateFieldName { get; private set; } = string.Empty;

    public static FormDefinitionEmailNotificationFieldMappingEntity Create(Guid formDefinitionId, FormDefinitionEmailNotificationFieldMappingDto model)
    {
        var entity = new FormDefinitionEmailNotificationFieldMappingEntity
        {
            FormDefinitionId = formDefinitionId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(FormDefinitionEmailNotificationFieldMappingDto model)
    {
        FormDefinitionFieldName = model.FormDefinitionFieldName;
        EmailTemplateFieldName = model.EmailTemplateFieldName;
    }
}

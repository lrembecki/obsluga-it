namespace lrembecki.core.settings.Entities;

internal class EmailTemplateFieldEntity
{
    public Guid EmailTemplateId { get; private set; }
    public string FieldName { get; private set; } = string.Empty;

    public static EmailTemplateFieldEntity Create(Guid emailTemplateId, string fieldName)
    {
        return new EmailTemplateFieldEntity
        {
            EmailTemplateId = emailTemplateId,
            FieldName = fieldName
        };
    }
}

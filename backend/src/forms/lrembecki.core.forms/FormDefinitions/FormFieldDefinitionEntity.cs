namespace lrembecki.core.forms.FormDefinitions;

internal class FormFieldDefinitionEntity
{
    public Guid FormDefinitionId { get; private set; }
    public string FieldName { get; private set; } = string.Empty;
    public string FieldType { get; private set; } = string.Empty;
    public bool IsRequired { get; private set; } = false;

    public static FormFieldDefinitionEntity Create(Guid formDefinitionId, FormFieldDefinitionDto dto)
    {
        return new FormFieldDefinitionEntity
        {
            FormDefinitionId = formDefinitionId,
            FieldName = dto.FieldName,
            FieldType = dto.FieldType,
            IsRequired = dto.IsRequired
        };
    }
}

namespace lrembecki.core.forms.FormDefinitions;

public record FormFieldDefinitionDto
{
    public string FieldName { get; init; } = string.Empty;
    public string FieldType { get; init; } = string.Empty;
    public bool IsRequired { get; init; } = false;
}

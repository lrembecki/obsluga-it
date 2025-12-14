namespace lrembecki.core.forms.FormDefinitions;

public record FormDefinitionDto
{
    public string Name { get; init; } = string.Empty;
    public List<FormFieldDefinitionDto> Fields { get; init; } = [];
}

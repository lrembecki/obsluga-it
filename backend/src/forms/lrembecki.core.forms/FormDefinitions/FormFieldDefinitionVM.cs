namespace lrembecki.core.forms.FormDefinitions;

public record FormFieldDefinitionVM(
    string FieldName,
    string FieldType,
    bool IsRequired
)
{
    internal static FormFieldDefinitionVM FromDto(FormFieldDefinitionEntity entity)
    {
        if (entity is null) return null!;

        return new(entity.FieldName, entity.FieldType, entity.IsRequired);
    }
}

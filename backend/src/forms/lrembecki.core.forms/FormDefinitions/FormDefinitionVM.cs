namespace lrembecki.core.forms.FormDefinitions;

public record FormDefinitionVM(
    Guid Id,
    string Name,
    List<FormFieldDefinitionVM> Fields
)
{
    internal static FormDefinitionVM Map(FormDefinitionEntity entity)
    {
        if (entity is null) return null!;

        return new(
            entity.Id,
            entity.Name,
            entity.Fields.Select(f => new FormFieldDefinitionVM(f.FieldName, f.FieldType, f.IsRequired)).ToList()
        );
    }
}
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


    public bool Validate(string value, out string errorMessage)
    {
        if (IsRequired && string.IsNullOrEmpty(value))
        {
            errorMessage = "Field is required";
            return false;
        }

        errorMessage = null!;
        return true;
    }
}
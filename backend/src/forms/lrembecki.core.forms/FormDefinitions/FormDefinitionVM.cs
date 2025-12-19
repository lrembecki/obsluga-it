using lrembecki.core.Exceptions;

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

    public void Validate(Dictionary<string, string> model)
    {
        var missingFields = Fields
            .Select(field =>
            {
                var value = model.GetValueOrDefault(field.FieldName);
                var validationResult = field.Validate(value!, out string errorMessage);

                return new
                {
                    field.FieldName, 
                    IsValid = validationResult, 
                    ErrorMessage = errorMessage
                };
            })
            .ToList();

        var invalidFields = missingFields
            .Where(f => !f.IsValid && f.ErrorMessage != null)
            .ToDictionary(f => f.FieldName, f => f.ErrorMessage!);

        if (invalidFields.Count > 0)
        {
            throw new ValidationException(invalidFields);
        }
    }
}
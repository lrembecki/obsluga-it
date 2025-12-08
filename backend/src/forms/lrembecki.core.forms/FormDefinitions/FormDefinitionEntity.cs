using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.forms.FormDefinitions;

public record FormFieldDefinitionDto
{
    public string FieldName { get; init; } = string.Empty;
    public string FieldType { get; init; } = string.Empty;
    public bool IsRequired { get; init; } = false;
}

internal class FormDefinitionEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public List<FormFieldDefinitionEntity> Fields { get; private set; } = [];
}

internal class FormFieldDefinitionEntity : SubscriptionBaseEntity
{
    public Guid FormDefinitionId { get; private set; }
    public string FieldName { get; private set; } = string.Empty;
    public string FieldType { get; private set; } = string.Empty;
    public bool IsRequired { get; private set; } = false;
}

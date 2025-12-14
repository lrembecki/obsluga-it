using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.forms.FormDefinitions;

internal class FormDefinitionEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public List<FormFieldDefinitionEntity> Fields { get; private set; } = [];

    public static FormDefinitionEntity Create(Guid id, FormDefinitionDto model)
        => new FormDefinitionEntity
        {
            Id = id,
            Name = model.Name
        };

    public void Update(FormDefinitionDto model)
    {
        Name = model.Name;
    }
}

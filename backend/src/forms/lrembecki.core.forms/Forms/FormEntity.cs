using lrembecki.core.Events;
using lrembecki.core.Markers;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.forms.Forms;

internal class FormEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public Guid FormDefinitionId { get; private set; }
    public List<FormFieldEntity> Fields { get; set; } = [];

    public static FormEntity Create(Guid id, FormDto model)
    {
        var entity = new FormEntity()
        {
            Id = id,
            FormDefinitionId = model.FormDefinitionId,
            Fields = model.Fields.Select(f => FormFieldEntity.Create(id, f.Key, f.Value)).ToList()
        };

        entity.AddDomainEvent(NotifyDomainEvent.Create(entity, id));

        return entity;
    }
}

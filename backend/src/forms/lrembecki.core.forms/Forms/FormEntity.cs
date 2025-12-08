using lrembecki.core.Entities;
using lrembecki.core.Markers;

namespace lrembecki.core.forms.Forms;

public class FormEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public Guid FormDefinitionId { get; private set; }
    public List<FormFieldEntity> Fields { get; set; } = [];

    public static FormEntity Create(Guid id, FormDto model)
        => new()
        {
            Id = id,
            FormDefinitionId = model.FormDefinitionId,
            Fields = model.Fields.Select(f => FormFieldEntity.Create(id, f.Key, f.Value)).ToList()
        };
}

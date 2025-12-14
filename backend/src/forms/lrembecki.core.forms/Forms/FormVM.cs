namespace lrembecki.core.forms.Forms;

public record FormVM(
    Guid Id,
    Guid FormDefinitionId,
    Dictionary<Guid, string> Fields
)
{
    public static FormVM Create(FormEntity entity)
    {
        if (entity is null) return null!;

        return new FormVM(
            entity.Id, 
            entity.FormDefinitionId,
            entity.Fields.ToDictionary(e => e.FormDefinitionFieldId, e => e.Value)
        );
    }
}
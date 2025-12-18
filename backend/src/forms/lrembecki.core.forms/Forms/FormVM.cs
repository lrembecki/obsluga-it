namespace lrembecki.core.forms.Forms;

public record FormVM(
    Guid Id,
    Guid FormDefinitionId,
    Dictionary<Guid, string> Fields
)
{
    internal static FormVM Map(FormEntity entity)
    {
        if (entity is null) return null!;

        return new FormVM(
            entity.Id, 
            entity.FormDefinitionId,
            entity.Fields.ToDictionary(e => e.FormDefinitionFieldId, e => e.Value)
        );
    }
}
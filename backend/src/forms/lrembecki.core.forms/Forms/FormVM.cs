namespace lrembecki.core.forms.Forms;

public record FormVM(
    Guid Id,
    Guid FormDefinitionId,
    DateTime CreatedAt,
    Dictionary<string, string> Fields
)
{
    internal static FormVM Map(FormEntity entity)
    {
        if (entity is null) return null!;

        return new FormVM(
            entity.Id, 
            entity.FormDefinitionId,
            entity.CreatedAt,
            entity.Fields.ToDictionary(e => e.Name, e => e.Value)
        );
    }
}
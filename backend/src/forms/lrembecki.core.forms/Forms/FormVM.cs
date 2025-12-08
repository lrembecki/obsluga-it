namespace lrembecki.core.forms.Forms;

public record FormVM(
    Guid Id,
    string Name,
    Dictionary<string, string> Fields
)
{
    public static FormVM Create(FormEntity entity)
    {
        if (entity is null) return null!;

        return new FormVM(
            entity.Id, 
            entity.Name, 
            entity.Fields.ToDictionary(e => e.Name, e => e.Value)
        );
    }
}
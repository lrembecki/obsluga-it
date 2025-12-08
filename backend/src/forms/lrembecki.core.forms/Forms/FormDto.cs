namespace lrembecki.core.forms.Forms;

public record FormDto
{
    public Guid FormDefinitionId { get; init; }
    public Dictionary<Guid, string> Fields { get; init; } = [];
}

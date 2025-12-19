namespace lrembecki.core.forms.Forms;

public record FormDto
{
    public Guid FormDefinitionId { get; init; }
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
    public Dictionary<string, string> Fields { get; init; } = [];
}

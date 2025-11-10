namespace lrembecki.core.trotamundos.Dtos;

public record TripHighlightDto
{
    public int Order { get; init; }
    public Guid HighlightId { get; init; }
    public string Value { get; init; } = string.Empty;
}

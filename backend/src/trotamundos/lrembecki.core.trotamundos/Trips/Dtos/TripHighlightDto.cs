namespace lrembecki.core.trotamundos.Trips.Dtos;

public record TripHighlightDto
{
    public int Order { get; init; }
    public Guid HighlightId { get; init; }
    public string Value { get; init; } = string.Empty;
}

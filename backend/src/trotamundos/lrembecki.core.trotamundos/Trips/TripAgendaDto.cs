namespace lrembecki.core.trotamundos.Trips;

public record TripAgendaDto
{
    public int Order { get; init; }
    public string Content { get; init; } = string.Empty;
}

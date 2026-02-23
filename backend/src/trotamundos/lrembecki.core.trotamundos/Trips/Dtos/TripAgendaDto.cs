namespace lrembecki.core.trotamundos.Trips.Dtos;

public record TripAgendaDto
{
    public int Order { get; init; }
    public string Content { get; init; } = string.Empty;
}

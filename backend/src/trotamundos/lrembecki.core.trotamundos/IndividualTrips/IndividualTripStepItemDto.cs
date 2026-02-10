namespace lrembecki.core.trotamundos.IndividualTrips;

public record IndividualTripStepItemDto
{
    public int Order { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

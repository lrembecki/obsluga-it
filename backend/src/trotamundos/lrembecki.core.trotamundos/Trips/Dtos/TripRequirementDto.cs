namespace lrembecki.core.trotamundos.Trips.Dtos;

public record TripRequirementDto
{
    public int Order { get; set; }
    public string Description { get; set; } = string.Empty;
}

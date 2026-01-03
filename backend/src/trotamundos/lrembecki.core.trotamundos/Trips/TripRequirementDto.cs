namespace lrembecki.core.trotamundos.Trips;

public record TripRequirementDto
{
    public int Order { get; set; }
    public string Description { get; set; } = string.Empty;
}

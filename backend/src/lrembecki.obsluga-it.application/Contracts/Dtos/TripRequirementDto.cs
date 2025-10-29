namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record TripRequirementDto
{
    public int Order { get; set; }
    public string Description { get; set; } = string.Empty;
}

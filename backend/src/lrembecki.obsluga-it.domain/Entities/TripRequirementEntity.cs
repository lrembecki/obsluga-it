namespace lrembecki.obsluga_it.domain.Entities;

internal class TripRequirementEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Description { get; private set; } = string.Empty;
}
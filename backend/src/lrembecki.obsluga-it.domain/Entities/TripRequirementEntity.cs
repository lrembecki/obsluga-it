namespace lrembecki.obsluga_it.domain.Entities;

internal class TripRequirementEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Description { get; private set; } = string.Empty;

    public static TripRequirementEntity Create(int order, string description)
    {
        var entity = new TripRequirementEntity();

        entity.Update(order, description);

        return entity;
    }

    public void Update(int order, string description)
    {
        Order = order;
        Description = description;
    }
}
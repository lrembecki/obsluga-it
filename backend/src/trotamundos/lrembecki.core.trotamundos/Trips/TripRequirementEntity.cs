namespace lrembecki.core.trotamundos.Trips;

public class TripRequirementEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Description { get; private set; } = string.Empty;

    public static TripRequirementEntity Create(Guid tripId, TripRequirementDto model)
    {
        var entity = new TripRequirementEntity
        {
            TripId = tripId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(TripRequirementDto model)
    {
        Order = model.Order;
        Description = model.Description;
    }
}
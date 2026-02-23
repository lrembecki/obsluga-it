using lrembecki.core.storage;
using lrembecki.core.trotamundos.Trips.Dtos;

namespace lrembecki.core.trotamundos.Trips.Entities;

internal class TripSuggestedFlightEntity
{
    public Guid TripId { get; private set; }
    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = default!;
    public int Order { get; private set; }

    public static TripSuggestedFlightEntity Create(Guid tripId, TripSuggestedFlightDto model)
    {
        var entity = new TripSuggestedFlightEntity
        {
            TripId = tripId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(TripSuggestedFlightDto model)
    {
        Order = model.Order;
        ImageId = model.ImageId!.Value;
    }
}

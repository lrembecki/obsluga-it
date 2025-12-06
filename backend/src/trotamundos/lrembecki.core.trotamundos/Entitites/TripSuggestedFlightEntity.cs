using lrembecki.core.storage.Entities;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class TripSuggestedFlightEntity
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

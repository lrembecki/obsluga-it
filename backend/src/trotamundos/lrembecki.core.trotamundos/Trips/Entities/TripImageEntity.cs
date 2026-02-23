using lrembecki.core.storage;
using lrembecki.core.trotamundos.Trips.Dtos;

namespace lrembecki.core.trotamundos.Trips.Entities;

internal class TripImageEntity
{
    public Guid TripId { get; private set; }
    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = default!;
    public int Order { get; private set; }

    public static TripImageEntity Create(Guid tripId, TripImageDto model)
    {
        var entity = new TripImageEntity()
        {
            TripId = tripId,
            ImageId = model.ImageId!.Value
        };

        entity.Update(model);

        return entity;
    }

    public void Update(TripImageDto model)
    {
        Order = model.Order;
    }
}

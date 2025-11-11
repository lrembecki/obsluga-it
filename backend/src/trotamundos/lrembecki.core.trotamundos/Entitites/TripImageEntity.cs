using lrembecki.core.storage.Entities;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class TripImageEntity
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

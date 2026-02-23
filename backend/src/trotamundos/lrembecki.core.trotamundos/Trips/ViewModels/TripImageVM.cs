using lrembecki.core.storage;
using lrembecki.core.trotamundos.Trips.Entities;

namespace lrembecki.core.trotamundos.Trips.ViewModels;

public record TripImageVM(
    Guid TripId,
    Guid ImageId,
    StorageVM Image,
    int Order
)
{
    internal static TripImageVM Map(TripImageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripImageVM(
            entity.TripId,
            entity.ImageId,
            StorageVM.Map(entity.Image),
            entity.Order
        );
    }
}

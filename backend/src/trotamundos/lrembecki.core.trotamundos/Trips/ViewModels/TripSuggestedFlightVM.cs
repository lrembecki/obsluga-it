using lrembecki.core.storage;
using lrembecki.core.trotamundos.Trips.Entities;

namespace lrembecki.core.trotamundos.Trips.ViewModels;

public record TripSuggestedFlightVM(
    Guid TripId,
    Guid ImageId,
    StorageVM Image,
    int Order
)
{
    internal static TripSuggestedFlightVM Map(TripSuggestedFlightEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripSuggestedFlightVM(
            entity.TripId,
            entity.ImageId,
            StorageVM.Map(entity.Image),
            entity.Order
        );
    }
}

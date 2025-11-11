using lrembecki.core.storage.ViewModels;
using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record TripImageVM(
    Guid TripId,
    StorageVM Image,
    int Order
)
{
    public static TripImageVM Map(TripImageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripImageVM(
            entity.TripId,
            StorageVM.Map(entity.Image),
            entity.Order
        );
    }
}


using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record TripImageVM(
    Guid TripId,
    Guid ImageId,
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
            entity.ImageId,
            entity.Order
        );
    }
}


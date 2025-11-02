using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record TripSuggestedFlightVM(
    Guid TripId,
    Guid ImageId,
    int Order
)
{
    public static TripSuggestedFlightVM Map(TripSuggestedFlightEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripSuggestedFlightVM(
            entity.TripId,
            entity.ImageId,
            entity.Order
        );
    }
}


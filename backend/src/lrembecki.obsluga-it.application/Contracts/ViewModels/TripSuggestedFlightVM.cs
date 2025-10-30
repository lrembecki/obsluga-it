using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.application.ViewModels;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record TripSuggestedFlightVM(
    Guid TripId,
    int Order,
    ImageBlobVM Image
)
{
    public static TripSuggestedFlightVM MapFromDomainEntity(TripSuggestedFlightEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripSuggestedFlightVM(
            entity.TripId,
            entity.Order,
            ImageBlobVM.MapFromDomainEntity(entity.Image)
        );
    }
}


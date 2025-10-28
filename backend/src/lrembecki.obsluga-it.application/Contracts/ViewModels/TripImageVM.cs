using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record TripImageVM(
    Guid TripId,
    int Order,
    bool IsMain,
    ImageBlobVM Image
)
{
    internal static TripImageVM MapFromDomainEntity(TripImageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripImageVM(
            entity.TripId,
            entity.Order,
            entity.IsMain,
            ImageBlobVM.MapFromDomainEntity(entity.Image)
        );
    }
}


using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record TripPriceIncludeVM(
    Guid TripId,
    int Order,
    bool Includes,
    string Content
)
{
    public static TripPriceIncludeVM MapFromDomainEntity(TripPriceIncludeEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripPriceIncludeVM(
            entity.TripId,
            entity.Order,
            entity.Includes,
            entity.Content
        );
    }
}


using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record TripRequirementVM(
    Guid TripId,
    int Order,
    string Description
)
{
    public static TripRequirementVM MapFromDomainEntity(TripRequirementEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripRequirementVM(
            entity.TripId,
            entity.Order,
            entity.Description
        );
    }
}


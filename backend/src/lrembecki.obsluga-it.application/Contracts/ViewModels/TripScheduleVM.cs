using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record TripScheduleVM(
    Guid TripId,
    int Order,
    string Title,
    string Content
)
{
    public static TripScheduleVM MapFromDomainEntity(TripScheduleEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripScheduleVM(
            entity.TripId,
            entity.Order,
            entity.Title,
            entity.Content
        );
    }
}


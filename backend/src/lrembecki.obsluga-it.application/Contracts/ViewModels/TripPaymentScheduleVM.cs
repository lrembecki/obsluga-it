using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record TripPaymentScheduleVM(
    Guid TripId,
    int Order,
    string Title,
    string Price,
    string Description
)
{
    public static TripPaymentScheduleVM MapFromDomainEntity(TripPaymentScheduleEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripPaymentScheduleVM(
            entity.TripId,
            entity.Order,
            entity.Title,
            entity.Price,
            entity.Description
        );
    }
}


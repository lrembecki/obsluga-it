using lrembecki.core.trotamundos.Trips.Entities;

namespace lrembecki.core.trotamundos.Trips.ViewModels;

public record TripPaymentScheduleVM(
    Guid TripId,
    int Order,
    string Title,
    string Price,
    string Description
)
{
    public static TripPaymentScheduleVM Map(TripPaymentScheduleEntity entity)
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

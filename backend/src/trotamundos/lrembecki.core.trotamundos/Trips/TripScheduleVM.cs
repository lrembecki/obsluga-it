namespace lrembecki.core.trotamundos.Trips;

public record TripScheduleVM(
    Guid TripId,
    int Order,
    string Title,
    string Content
)
{
    public static TripScheduleVM Map(TripScheduleEntity entity)
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

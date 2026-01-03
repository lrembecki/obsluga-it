namespace lrembecki.core.trotamundos.Trips;

public record TripHighlightVM(
    int Order,
    Guid HighlightId,
    string Value
)
{
    public static TripHighlightVM Map(TripHighlightEntity entity)
    {
        if (entity == null) return null!;

        return new TripHighlightVM(
            entity.Order,
            entity.HighlightId,
            entity.Value
        );
    }
}
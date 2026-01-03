namespace lrembecki.core.trotamundos.Trips;

public record TripAgendaVM(
    int Order,
    string Content
)
{
    public static TripAgendaVM Map(TripAgendaEntity entity)
        => new TripAgendaVM(entity.Order, entity.Content);
}

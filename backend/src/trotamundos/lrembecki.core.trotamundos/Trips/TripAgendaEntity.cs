namespace lrembecki.core.trotamundos.Trips;

public class TripAgendaEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Content { get; private set; } = string.Empty;

    public static TripAgendaEntity Create(Guid tripId, TripAgendaDto model)
    {
        var entity = new TripAgendaEntity { TripId = tripId };

        entity.Update(model);

        return entity;
    }

    public void Update(TripAgendaDto model)
    {
        Order = model.Order;
        Content = model.Content;
    }
}

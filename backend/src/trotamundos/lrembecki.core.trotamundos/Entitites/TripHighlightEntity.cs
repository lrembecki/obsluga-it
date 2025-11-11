using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class TripHighlightEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public Guid HighlightId { get; private set; }
    public string Value { get; private set; } = string.Empty;

    public static TripHighlightEntity Create(Guid tripId, TripHighlightDto model)
    {
        var entity = new TripHighlightEntity
        {
            TripId = tripId,
            HighlightId = model.HighlightId
        };

        entity.Update(model);
        
        return entity;
    }

    public void Update(TripHighlightDto model)
    {
        Order = model.Order;
        Value = model.Value;
    }
}

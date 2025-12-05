using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class TripPriceIncludeEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public bool Includes { get; private set; }
    public string Content { get; private set; } = string.Empty;

    public static TripPriceIncludeEntity Create(Guid tripId, TripPriceIncludeDto model)
    {
        var entity = new TripPriceIncludeEntity { TripId = tripId };

        entity.Update(model);

        return entity;
    }

    public void Update(TripPriceIncludeDto model)
    {
        Order = model.Order;
        Includes = model.Includes.GetValueOrDefault(false);
        Content = model.Content;
    }
}

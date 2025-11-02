using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class TripScheduleEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;

    public static TripScheduleEntity Create(Guid tripId, TripScheduleDto model)
    {
        var entity = new TripScheduleEntity
        {
            TripId = tripId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(TripScheduleDto model)
    {
        Order = model.Order;
        Title = model.Title;
        Content = model.Content;
    }
}

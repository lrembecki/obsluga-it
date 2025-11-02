using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class TripPaymentScheduleEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Price { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static TripPaymentScheduleEntity Create(Guid tripId, TripPaymentScheduleDto model)
    {
        var entity = new TripPaymentScheduleEntity
        {
            TripId = tripId
        };

        entity.Update(model);

        return entity;
    }

    public void Update(TripPaymentScheduleDto model)
    {
        Order = model.Order;
        Title = model.Title;
        Price = model.Price;
        Description = model.Description;
    }
}

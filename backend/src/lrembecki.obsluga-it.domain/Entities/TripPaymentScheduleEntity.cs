namespace lrembecki.obsluga_it.domain.Entities;

internal class TripPaymentScheduleEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Price { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static TripPaymentScheduleEntity Create(int order, string title, string price, string description)
    {
        var entity = new TripPaymentScheduleEntity();

        entity.Update(order, title, price, description);

        return entity;
    }

    public void Update(int order, string title, string price, string description)
    {
        Order = order;
        Title = title;
        Price = price;
        Description = description;
    }
}

namespace lrembecki.obsluga_it.domain.Entities;

internal class TripScheduleEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;

    public static TripScheduleEntity Create(int order, string title, string content)
    {
        var entity = new TripScheduleEntity();

        entity.Update(order, title, content);

        return entity;
    }

    public void Update(int order, string title, string content)
    {
        Order = order;
        Title = title;
        Content = content;
    }
}

namespace lrembecki.obsluga_it.domain.Entities;

public class TripPriceIncludeEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public bool Includes { get; private set; }
    public string Content { get; private set; } = string.Empty;

    public static TripPriceIncludeEntity Create(int order, bool includes, string content)
    {
        var entity = new TripPriceIncludeEntity();

        entity.Update(order, includes, content);

        return entity;
    }

    public void Update(int order, bool includes, string content)
    {
        Order = order;
        Includes = includes;
        Content = content;
    }
}

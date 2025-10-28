namespace lrembecki.obsluga_it.domain.Entities;

internal class TripSuggestedFlightEntity
{
    public Guid TripId { get; private set; }
    public Guid ImageId { get; private set; }
    public int Order { get; private set; }
    public ImageBlobEntity Image { get; private set; } = default!;

    public static TripSuggestedFlightEntity Create(int order, ImageBlobEntity image)
    {
        var entity = new TripSuggestedFlightEntity();

        entity.Update(order, image);

        return entity;
    }

    public void Update(int order, ImageBlobEntity image)
    {
        Order = order;
        Image = image;
    }
}

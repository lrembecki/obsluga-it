namespace lrembecki.obsluga_it.domain.Entities;

internal class TripSuggestedFlightEntity
{
    public Guid TripId { get; private set; }
    public Guid ImageId { get; private set; }
    public int Order { get; private set; }
    public ImageBlobEntity Image { get; private set; } = default!;
}

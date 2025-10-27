namespace lrembecki.obsluga_it.domain.Entities;

internal class TripPriceIncludeEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public bool Includes { get; private set; }
    public string Content { get; private set; } = string.Empty;
}

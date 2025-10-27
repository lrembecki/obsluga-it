namespace lrembecki.obsluga_it.domain.Entities;

internal class TripImageEntity
{
    public Guid TripId { get; private set; }
    public bool IsMain { get; private set; }
    public int Order { get; private set; }

    public static TripImageEntity Create(Guid tripId, bool isMain, int order)
    {
        return new TripImageEntity
        {
            TripId = tripId,
            IsMain = isMain,
            Order = order
        };
    }
}

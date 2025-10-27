using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class TripEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Subtitle { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public virtual List<TripImageEntity> Images { get; private set; } = [];
    public virtual List<TripHighlightEntity> Highlights { get; private set; } = [];
    public virtual List<TripAdvantageEntity> Advantages { get; private set; } = [];
    public virtual List<TripScheduleEntity> Schedules { get; private set; } = [];
    public virtual List<TripPriceIncludeEntity> PriceIncludes { get; private set; } = [];

    public static TripEntity Create(Guid tripId)
    {
        var entity = new TripEntity
        {
            Id = tripId
        };

        return entity;
    }
}

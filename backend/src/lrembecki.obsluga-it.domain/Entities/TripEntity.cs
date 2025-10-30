using lrembecki.shared.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

internal class TripEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Subtitle { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public virtual List<AdvantageEntity> Advantages { get; private set; } = [];
    public virtual List<HighlightEntity> Highlights { get; private set; } = [];
    public virtual List<TripImageEntity> Images { get; private set; } = [];
    public virtual List<TripPaymentScheduleEntity> PaymentSchedules { get; private set; } = [];
    public virtual List<TripPriceIncludeEntity> PriceIncludes { get; private set; } = [];
    public virtual List<TripRequirementEntity> Requirements { get; private set; } = [];
    public virtual List<TripScheduleEntity> Schedules { get; private set; } = [];
    public virtual List<TripSuggestedFlightEntity> SuggestedFlights { get; private set; } = [];

    // Factory method patterned after ActorEntity.Create
    public static TripEntity Create(Guid tripId, string title, string subtitle, string description)
    {
        var trip = new TripEntity
        {
            Id = tripId
        };
        trip.Update(title, subtitle, description);
        return trip;
    }

    // Update method patterned after ActorEntity.Update
    public void Update(string title, string subtitle, string description)
    {
        Title = title;
        Subtitle = subtitle;
        Description = description;
    }
}

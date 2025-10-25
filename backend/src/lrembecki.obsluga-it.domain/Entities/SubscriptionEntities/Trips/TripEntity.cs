using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Entities.SubscriptionEntities.BlobEntities;

namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities.Trips;

internal class TripEntity : SubscriptionEntity, IHasId<Guid>
{
    private readonly HashSet<TripImage> _images = [];
    private readonly HashSet<TripHighlight> _highlights = [];
    private readonly HashSet<TripAdvantages> _advantages = [];
    private readonly HashSet<TripSchedule> _schedules = [];
    private readonly HashSet<TripPriceIncludes> _priceIncludes = [];

    public Guid Id { get; private set; }

    public IReadOnlyCollection<TripImage> Images => _images.ToList().AsReadOnly();
    public IReadOnlyCollection<TripHighlight> Highlights => _highlights.ToList().AsReadOnly();
    public IReadOnlyCollection<TripAdvantages> Advantages => _advantages.ToList().AsReadOnly();
    public IReadOnlyCollection<TripSchedule> Schedules => _schedules.ToList().AsReadOnly();
    public IReadOnlyCollection<TripPriceIncludes> PriceIncludes => _priceIncludes.ToList().AsReadOnly();

    public static TripEntity Create(Guid tripId)
    {
        var entity = new TripEntity
        {
            Id = tripId
        };

        return entity;
    }
}


internal class GroupTripEntity : TripEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Subtitle { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
}

internal class TripImage
{
    public Guid TripId { get; private set; }
    public bool IsMain { get; private set; }
    public int Order { get; private set; }

    public static TripImage Create(Guid tripId, bool isMain, int order)
    {
        return new TripImage
        {
            TripId = tripId,
            IsMain = isMain,
            Order = order
        };
    }
}

internal class TripHighlight : SubscriptionEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Icon { get; private set; } = string.Empty;
}

internal class TripAdvantages : SubscriptionEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
}

internal class TripLoyalityProgram : SubscriptionEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public ImageBlobEntity Image { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

internal class TripSchedule
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
}

internal class TripPriceIncludes
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public bool Includes { get; private set; }
    public string Content { get; private set; } = string.Empty;
}

internal class TripSuggestedFlight
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public ImageBlobEntity Image { get; private set; } = default!;
}

internal class TripPaymentSchedule
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Price { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
}

internal class TripRequirement
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Description { get; private set; } = string.Empty;
}
using lrembecki.obsluga_it.domain.Events;

namespace lrembecki.obsluga_it.domain.Entities;

public class Subscription : BaseEntity, IHasId<Guid>
{
    private readonly HashSet<UserSubscription> _userSubscriptions = [];
    public IReadOnlyCollection<UserSubscription> UserSubscriptions => _userSubscriptions.AsReadOnly();

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static Subscription Create(Guid id, string name)
    {
        var subscription = new Subscription
        {
            Id = id,
            Name = name
        };

        subscription.AddDomainEvent(new SubscriptionCreatedDomainEvent(subscription.Id, subscription.Name));

        return subscription;
    }
}
using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Events;

namespace lrembecki.obsluga_it.domain.Entities;

public class SubscriptionEntity : BaseEntity, IHasId<Guid>
{
    private readonly HashSet<UserSubscriptionEntity> _userSubscriptions = [];
    public IReadOnlyCollection<UserSubscriptionEntity> UserSubscriptions => _userSubscriptions.ToList().AsReadOnly();

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static SubscriptionEntity Create(Guid id, string name)
    {
        var subscription = new SubscriptionEntity
        {
            Id = id,
            Name = name
        };

        subscription.AddDomainEvent(new SubscriptionCreatedDomainEvent(subscription.Id, subscription.Name));

        return subscription;
    }
}
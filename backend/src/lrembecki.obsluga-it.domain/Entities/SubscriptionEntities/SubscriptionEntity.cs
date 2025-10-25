using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;

public class SubscriptionEntity : BaseEntity, IHasSubscriptionId
{
    public Guid SubscriptionId { get; protected set; }
    public Subscription Subscription { get; protected set; } = null!;
}

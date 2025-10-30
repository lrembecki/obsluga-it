using lrembecki.shared.domain.Abstractions;

namespace lrembecki.shared.domain.Entities;

public class SubscriptionBaseEntity : BaseEntity, IHasSubscriptionId
{
    public Guid SubscriptionId { get; protected set; }
    public SubscriptionEntity Subscription { get; protected set; } = null!;
}

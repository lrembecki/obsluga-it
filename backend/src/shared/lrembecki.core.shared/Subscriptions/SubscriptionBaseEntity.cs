using lrembecki.core.Markers;

namespace lrembecki.core.shared.Subscriptions;

public class SubscriptionBaseEntity : BaseEntity, IHasSubscriptionId
{
    public Guid SubscriptionId { get; protected set; }
}

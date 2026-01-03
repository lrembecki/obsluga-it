using lrembecki.core.Markers;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.trotamundos.Common;

internal class TrotamundosBaseEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; protected set; }
}

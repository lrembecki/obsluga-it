using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.domain.Common;

internal class SubscriptionBaseEntity : BaseEntity, IHasSubscriptionId
{
    public Guid SubscriptionId { get; protected set; }
    public SubscriptionEntity Subscription { get; protected set; } = null!;
}

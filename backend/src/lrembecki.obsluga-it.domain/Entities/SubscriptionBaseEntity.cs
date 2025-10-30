using lrembecki.shared.domain.Abstractions;
using lrembecki.shared.domain.Entities;

namespace lrembecki.obsluga_it.domain.Entities;

internal class SubscriptionBaseEntity : BaseEntity, IHasSubscriptionId
{
    public Guid SubscriptionId { get; protected set; }
    public SubscriptionEntity Subscription { get; protected set; } = null!;
}

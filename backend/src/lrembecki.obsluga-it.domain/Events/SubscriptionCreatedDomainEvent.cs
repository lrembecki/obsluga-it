using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.domain.Events;

public record SubscriptionCreatedDomainEvent(Guid SubscriptionId, string Name)
    : DomainEvent(Guid.NewGuid(), nameof(SubscriptionEntity), DateTime.UtcNow);

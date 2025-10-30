using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.domain.Events;

namespace lrembecki.obsluga_it.domain.Events;

public record SubscriptionCreatedDomainEvent(Guid SubscriptionId, string Name)
    : DomainEvent(Guid.NewGuid(), nameof(SubscriptionEntity), DateTime.UtcNow);

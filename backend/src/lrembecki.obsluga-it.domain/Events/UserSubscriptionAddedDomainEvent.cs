using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.domain.Events;

namespace lrembecki.obsluga_it.domain.Events;

public record UserSubscriptionAddedDomainEvent(Guid UserId, Guid SubscriptionId)
    : DomainEvent(Guid.NewGuid(), nameof(UserEntity), DateTime.UtcNow);
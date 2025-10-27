using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.domain.Events;

public record UserSubscriptionAddedDomainEvent(Guid UserId, Guid SubscriptionId)
    : DomainEvent(Guid.NewGuid(), nameof(UserEntity), DateTime.UtcNow);
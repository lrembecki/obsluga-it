using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.domain.Events;

public record UserCreatedDomainEvent(Guid UserId, Email Email)
    : DomainEvent(Guid.NewGuid(), nameof(UserEntity), DateTime.UtcNow);

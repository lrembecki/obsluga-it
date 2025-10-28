using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.domain.Events;

public record UserCreatedDomainEvent(Guid UserId, string Email)
    : DomainEvent(Guid.NewGuid(), nameof(UserEntity), DateTime.UtcNow);

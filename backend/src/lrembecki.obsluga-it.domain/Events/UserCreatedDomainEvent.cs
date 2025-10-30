using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.domain.Events;

namespace lrembecki.obsluga_it.domain.Events;

public record UserCreatedDomainEvent(Guid UserId, string Email)
    : DomainEvent(Guid.NewGuid(), nameof(UserEntity), DateTime.UtcNow);

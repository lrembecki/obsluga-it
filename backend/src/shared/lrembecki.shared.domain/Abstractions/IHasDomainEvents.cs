using lrembecki.shared.domain.Events;

namespace lrembecki.shared.domain.Abstractions;

public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void AddDomainEvent(DomainEvent domainEvent);
    void ClearDomainEvents();
}
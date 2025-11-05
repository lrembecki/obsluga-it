using lrembecki.core.Events;

namespace lrembecki.core.Markers
{
    public interface IHasDomainEvents
    {
        IReadOnlyCollection<DomainEvent> DomainEvents { get; }
        void AddDomainEvent(DomainEvent domainEvent);
        void ClearDomainEvents();
    }
}
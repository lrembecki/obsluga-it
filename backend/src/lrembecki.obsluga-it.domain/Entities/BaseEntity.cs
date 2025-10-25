using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

public class BaseEntity : IHasDomainEvents, IHasAuditFields
{
    private readonly HashSet<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.ToList().AsReadOnly();

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public Guid? CreatedById { get; protected set; }
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
    public Guid? UpdatedById { get; protected set; }

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

namespace lrembecki.obsluga_it.domain.Entities;

public class BaseEntity : IHasDomainEvents, IHasAuditFields
{
    private readonly HashSet<DomainEvent> _domainEvents = [];    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; set;  }
    public DateTime UpdatedAt { get; set;  } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set;  }

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

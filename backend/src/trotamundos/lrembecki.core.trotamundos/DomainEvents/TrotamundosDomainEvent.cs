using lrembecki.core.Entities;
using lrembecki.core.Events;
using lrembecki.core.Markers;

namespace lrembecki.core.trotamundos.DomainEvents;

public record TrotamundosDomainEvent(
    Guid RecordId, 
    string EventType, 
    DateTime UtcNow) : PublishDomainEvent(
        Guid.NewGuid(), 
        EventType, 
        RecordId, 
        UtcNow
)
{
    public static DomainEvent Create<T>(T entity) where T : BaseEntity, IHasId<Guid>
        => new TrotamundosDomainEvent(
            entity.Id, 
            typeof(T).Name, 
            DateTime.UtcNow);
}

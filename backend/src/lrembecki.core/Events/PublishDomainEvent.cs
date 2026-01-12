using lrembecki.core.Markers;

namespace lrembecki.core.Events;

public record PublishDomainEvent(
    Guid MessageId, 
    string EventType, 
    Guid ExternalId, 
    DateTime OccurredOn
) : DomainEvent(
    MessageId, 
    "publish-entity", 
    EventType, 
    ExternalId, 
    OccurredOn
)
{
    public static PublishDomainEvent Create<T>(
        T entity
    ) where T : IHasId<Guid>
    {
        return new PublishDomainEvent(
            Guid.NewGuid(),
            typeof(T).Name,
            entity.Id,
            DateTime.UtcNow
        );
    }
}

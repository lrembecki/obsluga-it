namespace lrembecki.core.Events;

public record NotifyDomainEvent(Guid MessageId, string EventType, Guid ExternalId, DateTime OccuredOn)
    : DomainEvent(MessageId, "notify-entity", EventType, ExternalId, OccuredOn)
{
    public static NotifyDomainEvent Create<T>(T entity, Guid id)
        => new NotifyDomainEvent(Guid.NewGuid(), entity!.GetType().Name, id, DateTime.UtcNow);
}
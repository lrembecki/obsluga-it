namespace lrembecki.core.Events;

public record NotifyDomainEvent(Guid MessageId, string EventType, Guid ExternalId, DateTime OccuredOn)
    : DomainEvent(MessageId, "notify-entity", EventType, ExternalId, OccuredOn);
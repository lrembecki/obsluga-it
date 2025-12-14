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
);

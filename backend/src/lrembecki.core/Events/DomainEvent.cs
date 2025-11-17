namespace lrembecki.core.Events;

public record DomainEvent(
    Guid MessageId, 
    string Queue, 
    string EventType, 
    Guid ExternalId, 
    DateTime OccurredOn
);

namespace lrembecki.core.Events;

public record DomainEvent(
    Guid MessageId, 
    string Queue, 
    string EventType, 
    Guid ExternalId, 
    DateTime OccurredOn
)
{
    public Guid SubscriptionId { get; init; } = Guid.NewGuid();
}

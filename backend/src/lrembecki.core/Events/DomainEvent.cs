namespace lrembecki.core.Events
{
    public record DomainEvent(Guid Id, string EventType, DateTime OccurredOn);
}

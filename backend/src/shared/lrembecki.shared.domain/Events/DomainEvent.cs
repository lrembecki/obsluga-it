namespace lrembecki.shared.domain.Events;

public record DomainEvent(Guid Id, string EventType, DateTime OccurredOn);

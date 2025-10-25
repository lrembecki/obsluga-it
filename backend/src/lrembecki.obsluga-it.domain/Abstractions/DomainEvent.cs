namespace lrembecki.obsluga_it.domain.Abstractions;

public record DomainEvent(Guid Id, string EventType, DateTime OccurredOn);

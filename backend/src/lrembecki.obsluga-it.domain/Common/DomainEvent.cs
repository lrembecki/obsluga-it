namespace lrembecki.obsluga_it.domain.Common;

public record DomainEvent(Guid Id, string EventType, DateTime OccurredOn);

using lrembecki.core.Events;

namespace lrembecki.functions.trotamundos.Helpers;

public interface IPublisher
{
    Task Publish(DomainEvent domainEvent, CancellationToken ct = default);
}

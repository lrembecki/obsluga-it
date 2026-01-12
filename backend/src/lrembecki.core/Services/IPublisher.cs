using lrembecki.core.Events;

namespace lrembecki.core.Services;  

public interface IPublisher
{
    Task Publish(DomainEvent domainEvent, CancellationToken ct = default);
}

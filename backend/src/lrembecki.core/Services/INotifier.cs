using lrembecki.core.Events;

namespace lrembecki.core.Services;

public interface IEmailNotifier
{
    Task Notify(DomainEvent domainEvent, CancellationToken ct = default);
}
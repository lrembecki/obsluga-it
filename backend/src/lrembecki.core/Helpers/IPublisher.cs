using lrembecki.core.Events;

namespace lrembecki.core.Helpers;

public interface IPublisher
{
    void Notify<T>(T @event) where T : DomainEvent;
    Task PublishAsync();
}

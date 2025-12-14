using lrembecki.core.Events;

namespace lrembecki.core.Helpers;

public interface INotifier
{
    void Notify<T>(T @event) where T : DomainEvent;
    Task PublishAsync();
}

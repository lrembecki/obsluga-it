using lrembecki.shared.application.Abstractions;

namespace lrembecki.obsluga_it.infrastructure.Abstractions;

public interface IPublisher
{
    Task PublishAsync<T>(T notification, CancellationToken cancellationToken = default) where T : INotification;
}


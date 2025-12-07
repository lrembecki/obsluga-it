using lrembecki.core.Events;
using lrembecki.core.settings.Entities;
using lrembecki.core.trotamundos.Entitites;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.functions.trotamundos.Helpers;

internal sealed class PublishFactory(IServiceProvider provider) : IPublisher
{
    public Task Publish(DomainEvent domainEvent, CancellationToken ct)
        => domainEvent.EventType switch
        {
            nameof(TripEntity) => provider.GetRequiredService<TripPublisher>().Publish(domainEvent, ct),
            nameof(FileEntity) => provider.GetRequiredService<FilePublisher>().Publish(domainEvent, ct),
            nameof(ContactEntity) => provider.GetRequiredService<ContactPublisher>().Publish(domainEvent, ct),
            _ => throw new NotImplementedException()
        };
}

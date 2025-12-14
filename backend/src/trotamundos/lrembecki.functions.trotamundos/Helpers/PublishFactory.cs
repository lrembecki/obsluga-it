using lrembecki.core.Events;
using lrembecki.core.settings.Entities;
using lrembecki.core.trotamundos.Entitites;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.functions.trotamundos.Helpers;

internal sealed class PublishFactory(IServiceProvider provider) : IPublisher
{
    private readonly Dictionary<string, Type> _dictionary = new()
    {
        { nameof(TripEntity), typeof(TripPublisher) },
        { nameof(FileEntity), typeof(FilePublisher) },
        { nameof(ContactEntity), typeof(ContactPublisher) }
    };

    public Task Publish(DomainEvent domainEvent, CancellationToken ct)
        => (provider.GetRequiredService(_dictionary[domainEvent.EventType]) as IPublisher)!.Publish(domainEvent, ct);
}

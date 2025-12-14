using lrembecki.core.Events;
using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.forms.Forms;
using lrembecki.core.Services;
using lrembecki.core.settings.Entities;
using lrembecki.core.trotamundos.Entitites;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.function_app.Helpers;

internal sealed class PublishFactory(IServiceProvider provider) : IPublisher
{
    private readonly Dictionary<string, Type> _dictionary = new()
    {
        { nameof(TripEntity), typeof(TripPublisher) },
        { nameof(FileEntity), typeof(FilePublisher) },
        { nameof(ContactEntity), typeof(ContactPublisher) },
        { nameof(FormDefinitionEntity), typeof(FormDefinitionPublisher)  }
    };

    public Task Publish(DomainEvent domainEvent, CancellationToken ct)
        => (provider.GetRequiredService(_dictionary[domainEvent.EventType]) as IPublisher)!.Publish(domainEvent, ct);
}

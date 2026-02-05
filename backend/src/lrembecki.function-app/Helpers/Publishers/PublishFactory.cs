using lrembecki.core.Events;
using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.Services;
using lrembecki.core.settings.Contacts;
using lrembecki.core.settings.Website;
using lrembecki.core.trotamundos.Files;
using lrembecki.core.trotamundos.Pages.AboutUs;
using lrembecki.core.trotamundos.Pages.HowItWorks;
using lrembecki.core.trotamundos.Trips;

using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.function_app.Helpers.Publishers;

internal sealed class PublishFactory(IServiceProvider provider) : IPublisher
{
    private readonly Dictionary<string, Type> _dictionary = new()
    {
        { nameof(TripEntity), typeof(TripPublisher) },
        { nameof(FileEntity), typeof(FilePublisher) },
        { nameof(ContactEntity), typeof(ContactPublisher) },
        { nameof(FormDefinitionEntity), typeof(FormDefinitionPublisher)  },
        { nameof(AboutUsEntity), typeof(AboutUsPublisher) },
        { nameof(HowItWorksEntity), typeof(HowItWorksPublisher) },
        { nameof(WebsiteEntity), typeof(WebsitePublisher) },
        { "CompanyEntity", typeof(CompanyPublisher)  }
    };

    public Task Publish(DomainEvent domainEvent, CancellationToken ct)
        => (provider.GetRequiredService(_dictionary[domainEvent.EventType]) as IPublisher)!.Publish(domainEvent, ct);
}

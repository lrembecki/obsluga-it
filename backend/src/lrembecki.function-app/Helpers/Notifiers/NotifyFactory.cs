using lrembecki.core.Events;
using lrembecki.core.forms.Forms;
using lrembecki.core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.function_app.Helpers.Notifiers;

internal sealed class NotifyFactory(IServiceProvider provider) : IEmailNotifier
{
    private readonly Dictionary<string, Type> _dictionary = new()
    {
        { nameof(FormEntity), typeof(FormNotifier) },
    };
    public Task Notify(DomainEvent domainEvent, CancellationToken ct)
        => (provider.GetRequiredService(_dictionary[domainEvent.EventType]) as IEmailNotifier)!.Notify(domainEvent, ct);
}

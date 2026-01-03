using Azure.Messaging.ServiceBus;

using lrembecki.core.Events;
using lrembecki.core.Helpers;
using System.Text.Json;

namespace lrembecki.infrastructure.Helpers;

internal class Notifier(ServiceBusClient client) : INotifier
{
    private readonly HashSet<DomainEvent> _events = new();

    public void Notify<T>(T @event) where T : DomainEvent
    {
        _events.Add(@event);
    }

    public async Task PublishAsync()
    {
        if (_events.Count == 0)
            return;

        var sends = _events.ToList().Select(async e =>
        {
            var sender = client.CreateSender(e.Queue);
            var json = JsonSerializer.Serialize(e);

            var message = new ServiceBusMessage(json)
            {
                ContentType = "application/json",
                MessageId = e.MessageId.ToString()
            };

            await sender.SendMessageAsync(message);
        
            _events.Remove(e);
        });

        await Task.WhenAll(sends);
    }
}

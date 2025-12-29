using Azure.Messaging.ServiceBus;
using lrembecki.core.Events;
using lrembecki.core.Helpers;
using lrembecki.core.Services;
using lrembecki.functions.trotamundos;
using lrembecki.infrastructure.Helpers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace lrembecki.function_app;

public class NotifyDomainEventQueueFunction(
    ISessionAccessor session,
    ILogger<NotifyDomainEventQueueFunction> logger,
    IEmailNotifier notifier)
{

    [Function(nameof(NotifyDomainEventQueueFunction))]
    public async Task Run(
        [ServiceBusTrigger("notify-entity", Connection = "ServiceBus")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogInformation("Message ID: {id}", message.MessageId);
        logger.LogInformation("Message Body: {body}", message.Body);
        logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var domainEvent = message.Body.ToObjectFromJson<DomainEvent>()!;
        using (var context = session.CreateSessionContext(domainEvent.SubscriptionId))
        {
            await notifier.Notify(domainEvent, CancellationToken.None);
        }

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}
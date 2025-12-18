using Azure.Messaging.ServiceBus;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

using lrembecki.core.Events;
using lrembecki.core.Services;
using lrembecki.infrastructure.Helpers;


namespace lrembecki.functions.trotamundos;

public class PublishDomainEventEventQueueFunction(
    PredefinedSessionAccessor session,
    ILogger<PublishDomainEventEventQueueFunction> logger,
    IPublisher publisher)
{
    [Function(nameof(PublishDomainEventEventQueueFunction))]
    public async Task Run(
        [ServiceBusTrigger("publish-entity", Connection = "ServiceBus")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogInformation("Message ID: {id}", message.MessageId);
        logger.LogInformation("Message Body: {body}", message.Body);
        logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var domainEvent = message.Body.ToObjectFromJson<DomainEvent>()!;
        using (var context = session.CreateSessionContext(domainEvent.SubscriptionId))
        {
            await publisher.Publish(domainEvent, CancellationToken.None);
        }

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}
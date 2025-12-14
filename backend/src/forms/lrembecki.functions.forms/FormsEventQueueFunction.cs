using Azure.Messaging.ServiceBus;
using lrembecki.core.Events;
using lrembecki.core.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace lrembecki.functions.forms;

public class FormsEventQueueFunction(
    ILogger<FormsEventQueueFunction> logger,
    IPublisher publisher)
{

    [Function(nameof(FormsEventQueueFunction))]
    public async Task Run(
        [ServiceBusTrigger("forms", Connection = "ServiceBus")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogInformation("Message ID: {id}", message.MessageId);
        logger.LogInformation("Message Body: {body}", message.Body);
        logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var domainEvent = message.Body.ToObjectFromJson<DomainEvent>()!;
        await publisher.Publish(domainEvent, CancellationToken.None);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}
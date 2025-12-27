using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace lrembecki.function_app;

public class NotifyDomainEventQueueFunction
{
    private readonly ILogger<NotifyDomainEventQueueFunction> _logger;

    public NotifyDomainEventQueueFunction(ILogger<NotifyDomainEventQueueFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(NotifyDomainEventQueueFunction))]
    public async Task Run(
        [ServiceBusTrigger("notify-entity", Connection = "ServiceBus")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}
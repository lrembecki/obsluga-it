using Azure.Messaging.ServiceBus;
using lrembecki.core.Events;
using lrembecki.core.Helpers;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.Services;
using lrembecki.core.trotamundos.ViewModels;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace lrembecki.functions.trotamundos;

public class TrotamundosEventQueueFunction(
    ILogger<TrotamundosEventQueueFunction> logger,
    ITripService trips,
    IBlobHelper blob)
{
    [Function(nameof(TrotamundosEventQueueFunction))]
    public async Task Run(
        [ServiceBusTrigger("trotamundos", Connection = "ServiceBus")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogInformation("Message ID: {id}", message.MessageId);
        logger.LogInformation("Message Body: {body}", message.Body);
        logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var domainEvent = message.Body.ToObjectFromJson<DomainEvent>()!;

        await Publish(domainEvent, CancellationToken.None);

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }

    private Task Publish(DomainEvent domainEvent, CancellationToken cancellationToken = default)
        => domainEvent.EventType switch
        {
            nameof(TripEntity) => PublishTrips(domainEvent, cancellationToken),
            _ => throw new NotImplementedException()
        };

    private async Task PublishTrips(DomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var tripList = await trips.GetAllAsync(cancellationToken);
        var tripVM = await trips.GetByIdAsync(domainEvent!.ExternalId, cancellationToken);

        await Upload(tripVM, "trips", tripVM.Id.ToString());
        await Upload(tripList, "trips", "index");
    }

    private async Task Upload<T>(T data, string blobPath, string fileName)
    {
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
            blob.UploadBlobAsync(
                "trotamundos",
                $"data/{blobPath}/{fileName}",
                $"{fileName}.json",
                ms,
                [],
                default
            ).Wait();
    }
}
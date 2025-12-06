using Azure.Messaging.ServiceBus;
using lrembecki.core.Events;
using lrembecki.core.Helpers;
using lrembecki.core.settings.Entities;
using lrembecki.core.settings.Services;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.Services;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace lrembecki.functions.trotamundos;

public class TrotamundosEventQueueFunction(
    ILogger<TrotamundosEventQueueFunction> logger,
    ITripService trips,
    IFileService files,
    IContactService contacts,
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

    private Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
        => domainEvent.EventType switch
        {
            nameof(TripEntity) => PublishTrips(domainEvent, ct),
            nameof(FileEntity) => PublishFiles(domainEvent, ct),
            nameof(ContactEntity) => PublishContacts(domainEvent, ct),
            _ => throw new NotImplementedException()
        };

    private async Task PublishContacts(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var list = await contacts.GetAllAsync(ct);
        await Upload(list, "contacts", "index");
    }

    private async Task PublishTrips(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var tripList = await trips.GetAllAsync(ct);
        var tripVM = await trips.GetByIdAsync(domainEvent!.ExternalId, ct);

        await Upload(tripVM, "trips", tripVM.Id.ToString());
        await Upload(tripList, "trips", "index");
    }

    private async Task PublishFiles(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var fileVMs = await files.GetAllAsync(ct);
        await Upload(fileVMs, "files", "index");
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
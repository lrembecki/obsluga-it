using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using System.ComponentModel;

namespace lrembecki.obsluga_it.infrastructure.Services;

internal class BlobService(BlobServiceClient serviceClient) : IBlobService
{
    public async Task<T> SyncBlobDataAsync<T>(T model, string container, CancellationToken cancellationToken) where T : BlobDto
    {
        model.Id ??= Guid.NewGuid();

        if (model.BinaryData is null) return model;
        if (string.IsNullOrEmpty(model.Filename)) return model;

        model.BlobPath ??= $"{model.Id}/{model.Filename}";

        var binaryData = Convert.FromBase64String(model.BinaryData);
        using var ms = new MemoryStream();
        await ms.WriteAsync(binaryData, cancellationToken);

        var containerClient = serviceClient.GetBlobContainerClient(container);
        await containerClient.CreateIfNotExistsAsync(
                publicAccessType: PublicAccessType.Blob,
                cancellationToken: cancellationToken);

        var blobClient = containerClient.GetBlobClient(model.BlobPath);
        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = CalculateContentType(model.Filename),
            ContentDisposition = "inline"
        };

        await blobClient.DeleteIfExistsAsync(
                snapshotsOption: DeleteSnapshotsOption.None,
                cancellationToken: cancellationToken);

        await blobClient.UploadAsync(
                content: ms,
                httpHeaders: blobHttpHeaders,
                metadata: new Dictionary<string, string> { },
                cancellationToken: cancellationToken);

        model.BlobUrl = blobClient.Uri.ToString();
        model.BinaryData = null;

        return model;
    }

    public async Task RemoveBlobAsync(string blobPath, string container, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(blobPath)) return;

        var containerClient = serviceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(blobPath);

        await blobClient.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
    }

    private static string CalculateContentType(string filename)
    {
        var extension = Path.GetExtension(filename).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            ".txt" => "text/plain",
            ".html" => "text/html",
            ".json" => "application/json",
            ".doc" or ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => "application/octet-stream"
        };
    }
}

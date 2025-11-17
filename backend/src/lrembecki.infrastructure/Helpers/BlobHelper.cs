using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Helpers;

internal class BlobHelper(BlobServiceClient serviceClient) : IBlobHelper
{
    public async Task<string> UploadBlobAsync(string container, string blobName, string fileName, Stream data, Dictionary<string, string> metaData, CancellationToken cancellationToken)
    {
        var containerClient = serviceClient.GetBlobContainerClient(container);
        await containerClient.CreateIfNotExistsAsync(
                publicAccessType: PublicAccessType.Blob,
                cancellationToken: cancellationToken);

        var blobClient = containerClient.GetBlobClient(blobName);
        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = CalculateContentType(fileName),
            ContentDisposition = "inline"
        };

        await blobClient.DeleteIfExistsAsync(
                snapshotsOption: DeleteSnapshotsOption.None,
                cancellationToken: cancellationToken);

        await blobClient.UploadAsync(
                content: data,
                httpHeaders: blobHttpHeaders,
                metadata: new Dictionary<string, string> { },
                cancellationToken: cancellationToken);

        return blobClient.Uri.ToString();
    }

    public Task<Stream> DownloadBlobAsync(string container, string blobName, CancellationToken cancellationToken)
    {
        var containerClient = serviceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(blobName);
        return blobClient.OpenReadAsync(cancellationToken: cancellationToken);
    }

    public async Task RemoveBlobAsync(string blobName, string container, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(blobName)) return;

        var containerClient = serviceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(blobName);

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

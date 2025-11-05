namespace lrembecki.core.Helpers
{
    public interface IBlobHelper
    {
        Task<string> UploadBlobAsync(string container, string blobName, string fileName, Stream data, Dictionary<string, string> metaData, CancellationToken cancellationToken);
        Task<Stream> DownloadBlobAsync(string container, string blobName, CancellationToken cancellationToken);
        Task RemoveBlobAsync(string blobName, string container, CancellationToken cancellationToken);
    }
}

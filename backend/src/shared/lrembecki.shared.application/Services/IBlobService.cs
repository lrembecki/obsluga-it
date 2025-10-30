using lrembecki.shared.application.Dtos;

namespace lrembecki.obsluga_it.application.Services;

public interface IBlobService
{
    Task RemoveBlobAsync(string blobPath, string container, CancellationToken cancellationToken);
    Task<T> SyncBlobDataAsync<T>(T model, string container, CancellationToken cancellationToken) where T : BlobDto;
}

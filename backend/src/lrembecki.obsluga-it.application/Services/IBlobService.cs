using lrembecki.obsluga_it.application.Contracts.Dtos;

namespace lrembecki.obsluga_it.application.Services;

internal interface IBlobService
{
    Task RemoveBlobAsync(string blobPath, string container, CancellationToken cancellationToken);
    Task<T> SyncBlobDataAsync<T>(T model, string container, CancellationToken cancellationToken) where T : BlobDto;
}

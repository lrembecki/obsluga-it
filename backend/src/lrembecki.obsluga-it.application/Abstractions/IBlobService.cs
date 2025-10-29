using lrembecki.obsluga_it.application.Services;

namespace lrembecki.obsluga_it.application.Abstractions;

internal interface IBlobService
{
    Task<T> SyncBlobDataAsync<T>(T model, string container, CancellationToken cancellationToken) where T : BlobDto;
}

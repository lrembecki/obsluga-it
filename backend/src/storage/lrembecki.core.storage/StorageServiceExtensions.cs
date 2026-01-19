using lrembecki.core.storage.Markers;

namespace lrembecki.core.storage;

public static class StorageServiceExtensions
{
    public static async Task<TDto> SyncStorageAsync<TDto>(
        this TDto dto,
        IStorageService storageService,
        CancellationToken ct) 
        where TDto : IHasStorage<TDto>
    {
        var updateVM = await storageService.SyncAsync(dto.GetStorageId(), dto.GetStorage(), ct);
        return dto.UpdateStorageId(updateVM?.Id);
    }

    public static async Task<TDto> SyncStorageCollectionAsync<TDto, TItemDto>(
        this TDto model,
        IStorageService storage,
        Func<TDto, List<TItemDto>> selectCollection,
        CancellationToken ct)
        where TDto : IHasStorageCollection<TDto, TItemDto>
        where TItemDto : IHasStorage<TItemDto>
    {
        var collection = selectCollection(model);

        for (var i = 0; i < collection.Count; i++)
        {
            collection[i] = await collection[i].SyncStorageAsync(storage, ct);
        }

        return model.UpdateStorageCollection(collection);
    }
}
namespace lrembecki.core.storage.Markers;

public interface IHasStorageCollection<TDto, TCollectionItemDto>
    where TCollectionItemDto : IHasStorage<TCollectionItemDto>
{
    List<TCollectionItemDto> GetStoragecollection();
    TDto UpdateStorageCollection(List<TCollectionItemDto> items);
}
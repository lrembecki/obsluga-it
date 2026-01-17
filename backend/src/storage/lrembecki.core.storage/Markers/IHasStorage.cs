namespace lrembecki.core.storage.Markers;


public interface IHasStorage<TDto>
{
    Guid? GetStorageId();
    TDto UpdateStorageId(Guid? storageId);
    StorageDto GetStorage();
}
namespace lrembecki.core.storage;

internal class FileStorageEntity 
{
    public Guid StorageId { get; private set; }
    public string DisplayName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static FileStorageEntity Create(Guid storageId, FileStorageDto model)
    {
        var entity = new FileStorageEntity
        {
            StorageId = storageId
        };
    
        entity.Update(model);
    
        return entity;
    }

    public void Update(FileStorageDto model)
    {
        DisplayName = model.DisplayName;
        Description = model.Description;
    }
}

using lrembecki.core.storage.Dtos;

namespace lrembecki.core.storage.Entities;

public class ImageStorageEntity
{
    public Guid StorageId { get; private set; }
    public string? DisplayName { get; private set; } = string.Empty;
    public long Width { get; private set; }
    public long Height { get; private set; }

    public static ImageStorageEntity Create(Guid storageId, ImageStorageDto model)
    {
        var entity = new ImageStorageEntity
        {
            StorageId = storageId
        };
        
        entity.Update(model);
        
        return entity;
    }

    public void Update(ImageStorageDto model)
    {
        Width = model.Width;
        Height = model.Height;
        DisplayName = model.DisplayName;
    }
}

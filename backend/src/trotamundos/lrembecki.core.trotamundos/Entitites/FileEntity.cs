using lrembecki.core.storage.Entities;
using lrembecki.core.trotamundos.Common;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class FileEntity : TrotamundosBaseEntity
{
    public Guid StorageId { get; private set; }
    public StorageEntity Storage { get; private set; } = default!;

    public string Group { get; private set; } = string.Empty;
    public static FileEntity Create(Guid storageId, FileDto model)
    {
        var entity = new FileEntity { StorageId = storageId };
        
        entity.Update(model);
        
        return entity;
    }

    public void Update(FileDto model)
    {
        StorageId = model.StorageId;
        Group = model.Group;
    }
}

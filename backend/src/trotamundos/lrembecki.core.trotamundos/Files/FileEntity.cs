using lrembecki.core.storage;
using lrembecki.core.trotamundos.Common;
using lrembecki.core.trotamundos.Trips;

namespace lrembecki.core.trotamundos.Files;

internal class FileEntity : TrotamundosBaseEntity
{
    public Guid StorageId { get; private set; }
    public StorageEntity Storage { get; private set; } = default!;

    public int Position { get; private set; }
    public string DisplayName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Group { get; private set; } = string.Empty;

    public static FileEntity Create(Guid storageId, FileDto model)
    {
        var entity = new FileEntity { StorageId = storageId };
    
        entity.Update(model);

        entity.AddDomainEvent(TrotamundosDomainEvent.Create(entity));
    
        return entity;
    }

    public void Update(FileDto model)
    {
        Position = model.Position;
        StorageId = model.StorageId;
        Group = model.Group;
        DisplayName = model.DisplayName;
        Description = model.Description;


        AddDomainEvent(TrotamundosDomainEvent.Create(this));
    }
}

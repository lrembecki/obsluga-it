using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Files;

public record FileVM(
    Guid Id,
    string Group,
    int Position,
    string DisplayName,
    string Description,
    Guid StorageId,
    StorageVM Storage)
{
    internal static FileVM Map(FileEntity entity)
    {
        if (entity == null) return null!;

        return new FileVM(
            entity.Id,
            entity.Group,
            entity.Position,
            entity.DisplayName,
            entity.Description,
            entity.StorageId,
            StorageVM.Map(entity.Storage));
    }
    
}

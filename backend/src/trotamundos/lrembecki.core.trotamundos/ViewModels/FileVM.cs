using lrembecki.core.storage.ViewModels;
using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record FileVM(
    Guid Id,
    string Group)
{
    public StorageVM Storage { get; init; } = default!;

    public static FileVM Map(FileEntity entity)
    {
        if (entity == null) return null!;

        return new FileVM(
            entity.Id,
            entity.Group);
    }
        
}

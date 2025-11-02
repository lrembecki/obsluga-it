using lrembecki.core.storage.Entities;

namespace lrembecki.core.storage.ViewModels;

public record FileStorageVM(string DisplayName, string Description)
{
    internal static FileStorageVM Map(FileStorageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new FileStorageVM(
            entity.DisplayName,
            entity.Description
        );
    }
}


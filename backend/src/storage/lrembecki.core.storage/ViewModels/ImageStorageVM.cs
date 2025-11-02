using lrembecki.core.storage.Entities;

namespace lrembecki.core.storage.ViewModels;

public record ImageStorageVM(long Width, long Height)
{
    internal static ImageStorageVM Map(ImageStorageEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }
        return new ImageStorageVM(
            entity.Width,
            entity.Height
        );
    }
}


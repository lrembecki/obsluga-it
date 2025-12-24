using lrembecki.core.storage.ViewModels;
using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record AboutUsVM(
    Guid Id,
    Guid ImageId,
    StorageVM Image,
    string Title,
    string Description
)
{
    internal static AboutUsVM Map(AboutUsEntity entity)
    {
        if (entity is null)
        {
            return null!;
        }

        return new AboutUsVM(
            entity.Id,
            entity.ImageId,
            StorageVM.Map(entity.Image),
            entity.Title,
            entity.Description
        );
    }
}

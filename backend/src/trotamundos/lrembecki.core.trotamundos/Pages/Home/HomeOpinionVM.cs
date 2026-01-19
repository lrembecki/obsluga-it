using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.Home;

public record HomeOpinionVM(
    int Order,
    Guid ImageId,
    StorageVM Image
)
{
    internal static HomeOpinionVM Map(HomeOpinionEntity entity)
    {
        if (entity == null) return null!;

        return new(
            entity.Order,
            entity.ImageId,
            StorageVM.Map(entity.Image)
        );
    }
}

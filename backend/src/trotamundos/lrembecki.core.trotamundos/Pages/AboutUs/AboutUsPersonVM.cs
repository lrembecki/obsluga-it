using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsPersonVM(
    int Order,
    string Name,
    string Description,
    Guid ImageId,
    StorageVM Image
) 
{ 
    internal static AboutUsPersonVM Map(AboutUsPersonEntity entity)
    {
        if (entity == null) return null!;

        return new(
            entity.Order,
            entity.Name,
            entity.Description,
            entity.ImageId,
            StorageVM.Map(entity.Image)
        );
    }
}

using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public record HowItWorksItemVM(
    int Order,
    string Title,
    string Description,
    Guid? ImageId,
    StorageVM? Image
)
{
    internal static HowItWorksItemVM? Map(HowItWorksItemEntity? entity)
    {
        if (entity == null) return null;
        return new(
            entity.Order,
            entity.Title,
            entity.Description,
            entity.ImageId,
            entity.Image != null ? StorageVM.Map(entity.Image) : null
        );
    }
}

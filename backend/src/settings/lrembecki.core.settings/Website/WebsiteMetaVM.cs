using lrembecki.core.storage;

namespace lrembecki.core.settings.Website;

public record WebsiteMetaVM(
    string Title,
    string Description,
    string Keywords,
    Guid ImageId,
    StorageVM Image
)
{
    internal static WebsiteMetaVM Map(WebsiteMetaEntity entity)
    {
        if (entity is null) return null!;

        return new WebsiteMetaVM(
            entity.Title,
            entity.Description,
            entity.Keywords,
            entity.Image.Id,
            StorageVM.Map(entity.Image)
        );
    }
}

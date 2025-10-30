using lrembecki.shared.domain.Entities;

namespace lrembecki.shared.application.ViewModels;

public record ImageBlobVM(
    Guid Id,
    string? Filename,
    string? BlobUrl,
    string? BlobPath,
    long? Size,
    string? DisplayName,
    string? Description,
    long? Width,
    long? Heiht)
{
    public static ImageBlobVM MapFromDomainEntity(ImageBlobEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new ImageBlobVM(
            entity.Id,
            entity.Filename,
            entity.BlobUrl,
            entity.BlobPath,
            entity.Size,
            entity.DisplayName,
            entity.Description,
            entity.Width,
            entity.Height
        );
    }
}


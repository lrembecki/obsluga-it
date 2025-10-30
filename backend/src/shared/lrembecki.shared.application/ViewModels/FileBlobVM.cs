using lrembecki.shared.domain.Entities;

namespace lrembecki.shared.application.ViewModels;

public record FileBlobVM(
    Guid Id,
    string? Filename,
    string? BlobUrl,
    string? BlobPath,
    long? Size,
    string? DisplayName,
    string? Description,
    int Postion,
    Guid? GroupId)
{
    public static FileBlobVM MapFromDomainEntity(FileBlobEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new FileBlobVM(
            entity.Id,
            entity.Filename,
            entity.BlobUrl,
            entity.BlobPath,
            entity.Size,
            entity.DisplayName,
            entity.Description,
            entity.Position,
            entity.GroupId
        );
    }
}


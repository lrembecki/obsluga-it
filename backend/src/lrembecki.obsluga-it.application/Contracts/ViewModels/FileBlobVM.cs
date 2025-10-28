using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

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
    internal static FileBlobVM MapFromDomainEntity(FileBlobEntity entity)
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


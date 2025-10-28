using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class FileBlobEntity : BlobBaseEntity
{
    public string? DisplayName { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Position { get; private set; }

    public Guid? GroupId { get; set; }
    public FileGroupEntity? Group { get; set; } = default!;

    public static FileBlobEntity Create(Guid fileId, string displayName, string description, int position, FileGroupEntity fileGroup, BlobBaseEntity blob)
    {
        var entity = Create<FileBlobEntity>(fileId, blob?.Filename, blob?.BlobUrl, blob?.BlobPath, blob?.Size);
        entity.Update(description, displayName, position, fileGroup, blob);

        return entity;
    }

    public void Update(
        string description,
        string displayName,
        int position, 
        FileGroupEntity? group,
        BlobBaseEntity? blob)
    {
        if (blob is not null) Update(blob);

        Description = description;
        DisplayName = displayName;
        Position = position;
        GroupId = group?.Id;
        Group = group;
    }
}

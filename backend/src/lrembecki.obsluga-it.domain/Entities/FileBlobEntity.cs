using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class FileBlobEntity : BlobBaseEntity
{
    public string? DisplayName { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Position { get; private set; }

    public Guid? GroupId { get; set; }
    public FileGroupEntity? Group { get; set; } = default!;

    public static FileBlobEntity Create(Guid fileId) => Create<FileBlobEntity>(fileId, null!, null!, null!, null);

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

using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class FileBlobEntity : BlobBaseEntity
{
    public string? DisplayName { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Position { get; private set; }

    public Guid? GroupId { get; set; }
    public FileGroupEntity? Group { get; set; } = default!;

    public void Update(
        string description,
        string displayName,
        int position,
        FileGroupEntity? group)
    {
        Description = description;
        DisplayName = displayName;
        Position = position;
        GroupId = group?.Id;
        Group = group;
    }
}

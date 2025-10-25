namespace lrembecki.obsluga_it.domain.Entities.SubscriptionEntities.BlobEntities;

public class FileBlobEntity : BlobEntity
{
    public string? DisplayName { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Position { get; private set; }

    public Guid? GroupId { get; set; }
    public FileGroup? Group { get; set; } = default!;

    public static FileBlobEntity Create(Guid fileId) => Create<FileBlobEntity>(fileId, null!, null!, null!, null);

    public void Update(
        string description,
        string displayName,
        int position, 
        FileGroup? group,
        BlobEntity? blob)
    {
        if (blob is not null) Update(blob);

        Description = description;
        DisplayName = displayName;
        Position = position;
        GroupId = group?.Id;
        Group = group;
    }
}

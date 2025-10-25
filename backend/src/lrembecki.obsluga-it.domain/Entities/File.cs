using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

public class File : IHasAuditFields, IHasSubscriptionId
{
    public Guid FileId { get; private set; }
    public string Name { get; private set; } = default!;
    public string? DisplayName { get; private set; } = default!;
    public string? Description { get; private set; }
    public string BlobPath { get; private set; } = default!;
    public string Url { get; private set; } = default!;
    public long Filesize { get; private set; } = default!;
    public int Position { get; private set; }

    public Guid? GroupId { get; set; }
    public FileGroup? Group { get; set; } = default!;

    public Guid SubscriptionId { get; set; } = Guid.Empty;
    public Guid? CreatedById { get; set; } = Guid.Empty;
    public DateTime CreatedAt { get; set; } = default!;
    public Guid? UpdatedById { get; set; } = Guid.Empty;
    public DateTime UpdatedAt { get; set; } = default!;

    private File() { }

    public static File Create(Guid fileId)
    {
        var file = new File
        {
            FileId = fileId
        };

        return file;
    }

    public void Update(string displayname, int position)
    {
        DisplayName = displayname;
        Position = position;
    }

    public void Update(string name, string description, string blobPath, string url, long fileSize)
    {
        Name = name;
        Description = string.IsNullOrWhiteSpace(description) ? null : description;
        BlobPath = blobPath;
        Url = url;
        Filesize = fileSize;
    }

    public void Update(FileGroup? group)
    {
        GroupId = group?.FileGroupId;
        Group = group;
    }
}

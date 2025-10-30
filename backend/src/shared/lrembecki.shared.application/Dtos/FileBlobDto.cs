namespace lrembecki.shared.application.Dtos;

public record FileBlobDto : BlobDto
{
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Position { get; set; }
    public Guid? FileGroupId { get; set; }
}

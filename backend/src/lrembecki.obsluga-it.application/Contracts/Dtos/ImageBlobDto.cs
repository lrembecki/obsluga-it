namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record ImageBlobDto : BlobDto
{
    public string? DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public long? Width { get; set; }
    public long? Height { get; set; }
}
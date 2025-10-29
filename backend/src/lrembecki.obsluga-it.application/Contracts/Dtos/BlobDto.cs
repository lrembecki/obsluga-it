namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record BlobDto
{
    public Guid? Id { get; set; }
    public string? BinaryData { get; set; }
    public string? Filename { get; set; }
    public string? BlobUrl { get; set; }
    public string? BlobPath { get; set; }
    public long? Size { get; set; }
}

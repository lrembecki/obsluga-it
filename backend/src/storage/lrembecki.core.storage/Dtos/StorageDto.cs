namespace lrembecki.core.storage.Dtos;

public record StorageDto
{
    public string? BinaryData { get; init; }
    public string? Filename { get; init; }
    public string? BlobUrl { get; init; }
    public string? BlobPath { get; init; }
    public long? Size { get; init; }
    public FileStorageDto File { get; init; } = default!;
    public ImageStorageDto Image { get; init; } = default!;
}
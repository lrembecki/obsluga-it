namespace lrembecki.core.storage;

public record ImageStorageDto
{
    public long Width { get; init; }
    public long Height { get; init; }
    public string DisplayName { get; init; } = default!;
}

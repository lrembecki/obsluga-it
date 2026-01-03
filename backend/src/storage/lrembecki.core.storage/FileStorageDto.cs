namespace lrembecki.core.storage;

public record FileStorageDto
{
    public string DisplayName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

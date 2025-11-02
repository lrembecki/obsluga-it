namespace lrembecki.core.storage.Dtos;

public record FileStorageDto
{
    public string DisplayName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

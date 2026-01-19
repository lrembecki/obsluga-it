using lrembecki.core.storage;
using lrembecki.core.storage.Markers;

namespace lrembecki.core.website.Settings;

public record SettingsDto : IHasStorage<SettingsDto>
{
    public string PrimaryColor { get; init; } = string.Empty;
    public string SecondaryColor { get; init; } = string.Empty;
    public string FontFamily { get; init; } = string.Empty;
    public string FontSize { get; init; } = string.Empty;
    public Guid? FontFileId { get; init; }
    public StorageDto? FontFile { get; init; } = null!;

    public StorageDto GetStorage()
        => FontFile!;

    public Guid? GetStorageId()
        => FontFileId;

    public SettingsDto UpdateStorageId(Guid? storageId)
        => this with { FontFileId = storageId };
}

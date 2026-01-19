using lrembecki.core.storage;

namespace lrembecki.core.website.Settings;

public record SettingsVM(
    Guid Id,
    string PrimaryColor,
    string SecondaryColor,
    string FontFamily,
    string FontSize,
    StorageVM? FontFile
)
{
    internal static SettingsVM Map(SettingsEntity entity)
    {
        if (entity == null) return null!;

        return new SettingsVM(
            entity.Id,
            entity.PrimaryColor,
            entity.SecondaryColor,
            entity.FontFamily,
            entity.FontSize,
            StorageVM.Map(entity.FontFile!)
        );
    }
}

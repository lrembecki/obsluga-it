using lrembecki.core.Markers;
using lrembecki.core.shared.Subscriptions;
using lrembecki.core.storage;


namespace lrembecki.core.website.Settings;

internal class SettingsEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; protected set; }
    
    public string PrimaryColor { get; private set; } = string.Empty;
    public string SecondaryColor { get; private set; } = string.Empty;
    public string FontFamily { get; private set; } = string.Empty;
    public string FontSize { get; private set; } = string.Empty;

    public Guid? FontFileId { get; private set; }
    public StorageEntity FontFile { get; private set; } = null!;

    internal static SettingsEntity Create(Guid id, SettingsDto dto)
    {
        var entity = new SettingsEntity
        {
            Id = id
        };

        entity.Update(dto);
        
        return entity;
    }

    public void Update(SettingsDto dto)
    {
        PrimaryColor = dto.PrimaryColor;
        SecondaryColor = dto.SecondaryColor;
        FontFamily = dto.FontFamily;
        FontSize = dto.FontSize;
        FontFileId = dto.FontFileId;
    }
}

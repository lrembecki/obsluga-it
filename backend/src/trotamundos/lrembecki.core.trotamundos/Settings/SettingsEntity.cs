using lrembecki.core.settings.Contacts;
using lrembecki.core.trotamundos.Common;

namespace lrembecki.core.trotamundos.Settings;

internal class SettingsEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public ContactEntity Email { get; private set; } = null!;
}
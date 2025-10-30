using lrembecki.shared.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

internal class HighlightEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Icon { get; private set; } = string.Empty;

    public static HighlightEntity Create(Guid id, string title, string icon)
        => new HighlightEntity
        {
            Id = id,
            Title = title,
            Icon = icon
        };

    public void Update(string title, string icon)
    {
        Title = title;
        Icon = icon;
    }
}

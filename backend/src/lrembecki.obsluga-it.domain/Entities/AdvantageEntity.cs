using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class AdvantageEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;

    public static AdvantageEntity Create(Guid id, string title, string content)
        => new AdvantageEntity
        {
            Id = id,
            Title = title,
            Content = content
        };

    public void Update(string title, string content)
    {
        Title = title;
        Content = content;
    }
}

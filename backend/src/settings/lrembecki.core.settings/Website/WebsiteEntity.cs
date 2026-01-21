using lrembecki.core.Events;
using lrembecki.core.Markers;
using lrembecki.core.settings.Companies;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.settings.Website;

internal class WebsiteEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public WebsiteMetaEntity Meta { get; private set; } = null!;
    public Guid CompanyId { get; private set; }
    public CompanyEntity Company { get; private set; } = null!;

    public static WebsiteEntity Create(
        Guid id,
        WebsiteDto  model
    )
    {
        var entity = new WebsiteEntity
        {
            Id = id
        };

        entity.Update(model);

        return entity;
    }

    public void Update(WebsiteDto model)
    {
        // Properties
        Title = model.Title;
        Url = model.Url;

        // References
        CompanyId = model.CompanyId;

        // Meta
        Meta ??= WebsiteMetaEntity.Create(Id, model.Meta);
        Meta.Update(model.Meta);

        AddDomainEvent(PublishDomainEvent.Create(this));
    }
}

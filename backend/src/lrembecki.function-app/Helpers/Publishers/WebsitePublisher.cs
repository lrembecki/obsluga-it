using lrembecki.core.Events;
using lrembecki.core.Services;
using lrembecki.core.settings.Website;

namespace lrembecki.function_app.Helpers.Publishers;

internal sealed class WebsitePublisher(
    IWebsiteService website,
    UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var websiteVM = await website.GetAllAsync(ct);
        await uploadHelper.Upload(websiteVM, domainEvent.SubscriptionId.ToString(), "settings", "website");
    }
}
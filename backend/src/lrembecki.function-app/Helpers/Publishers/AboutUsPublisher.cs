using lrembecki.core.Events;
using lrembecki.core.Services;
using lrembecki.core.trotamundos.Pages.AboutUs;

namespace lrembecki.function_app.Helpers.Publishers;

internal sealed class AboutUsPublisher(
    ISender sender,
    UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var aboutUsVM = await sender.Send(new AboutUsGetAllRequest(), ct);
        await uploadHelper.Upload(aboutUsVM, domainEvent.SubscriptionId.ToString(), "pages", "about-us");
    }
}

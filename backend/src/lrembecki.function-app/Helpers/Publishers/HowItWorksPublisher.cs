using lrembecki.core.Events;
using lrembecki.core.Services;
using lrembecki.core.trotamundos.Pages.HowItWorks;

namespace lrembecki.function_app.Helpers.Publishers;

internal sealed class HowItWorksPublisher(
    ISender sender,
    UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var howItWorksVM = await sender.Send(new HowItWorksGetAllRequest(), ct);
        await uploadHelper.Upload(howItWorksVM, domainEvent.SubscriptionId.ToString(), "pages", "how-it-works");
    }
}

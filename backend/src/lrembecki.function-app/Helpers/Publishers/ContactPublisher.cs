using lrembecki.core.Events;
using lrembecki.core.Services;
using lrembecki.core.settings.Contacts;

namespace lrembecki.function_app.Helpers.Publishers;

internal sealed class ContactPublisher(
    IContactService contacts,
    UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var list = await contacts.GetAllAsync(ct);
        await uploadHelper.Upload(list, domainEvent.SubscriptionId.ToString(), "contacts", "index");
    }
}
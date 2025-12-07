using lrembecki.core.Events;
using lrembecki.core.settings.Services;

namespace lrembecki.functions.trotamundos.Helpers;

internal sealed class ContactPublisher(
    IContactService contacts,
    UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var list = await contacts.GetAllAsync(ct);
        await uploadHelper.Upload(list, "contacts", "index");
    }
}
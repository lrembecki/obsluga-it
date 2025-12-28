using lrembecki.core.Events;
using lrembecki.core.Services;
using lrembecki.core.trotamundos.Services;

namespace lrembecki.function_app.Helpers.Publishers;

internal sealed class FilePublisher(
    IFileService files,
    UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var list = await files.GetAllAsync(ct);
        await uploadHelper.Upload(list, domainEvent.SubscriptionId.ToString(), "files", "index");
    }
}

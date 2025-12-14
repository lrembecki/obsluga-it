using lrembecki.core.Events;
using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.Services;

namespace lrembecki.function_app.Helpers;

internal sealed class FormDefinitionPublisher(
    IFormDefinitionService formDefinitions,
    UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var list = await formDefinitions.GetAllAsync(ct);
        var entity = list.Find(e => e.Id == domainEvent.ExternalId)!;

        await uploadHelper.Upload(list, domainEvent.SubscriptionId.ToString(), "form-definitions", "index");
        await uploadHelper.Upload(entity, domainEvent.SubscriptionId.ToString(), "form-definitions", $"{entity.Id}");
    }
}
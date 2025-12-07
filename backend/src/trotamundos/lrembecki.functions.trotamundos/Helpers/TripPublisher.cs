using lrembecki.core.Events;
using lrembecki.core.trotamundos.Services;

namespace lrembecki.functions.trotamundos.Helpers;

internal sealed class TripPublisher(ITripService trips, UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var tripList = await trips.GetAllAsync(ct);
        var tripVM = await trips.GetByIdAsync(domainEvent!.ExternalId, ct);

        await uploadHelper.Upload(tripVM, "trips", tripVM.Id.ToString());
        await uploadHelper.Upload(tripList, "trips", "index");
    }
}

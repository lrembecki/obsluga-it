using lrembecki.core.Events;
using lrembecki.core.Services;
using lrembecki.core.trotamundos.Trips;

namespace lrembecki.function_app.Helpers.Publishers;

internal sealed class TripPublisher(ITripService trips, UploadHelper uploadHelper) : IPublisher
{
    public async Task Publish(DomainEvent domainEvent, CancellationToken ct = default)
    {
        var tripList = await trips.GetAllAsync(ct);
        await uploadHelper.Upload(tripList, domainEvent.SubscriptionId.ToString(), "trips", "index");

        var tripVM = await trips.GetByIdAsync(domainEvent!.ExternalId, ct);
        if (tripVM is not null )
        {
            await uploadHelper.Upload(tripVM, domainEvent.SubscriptionId.ToString(), "trips", domainEvent!.ExternalId.ToString());
        } 
        else
        {
            await uploadHelper.Remove(domainEvent.SubscriptionId.ToString(), "trips", domainEvent.ExternalId.ToString());
        }
    }
}

using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsNestedCollections()
    {
        var tripId = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(TripEntity), true)!;
        Set(entity, "Id", tripId);
        Set(entity, "Title", "Trip");
        Set(entity, "Subtitle", "Sub");
        Set(entity, "Description", "Desc");

        var advantage = AdvantageEntity.Create(Guid.NewGuid(), "Adv", "Adv C");
        var highlight = HighlightEntity.Create(Guid.NewGuid(), "High", "icon");
        var tripImage = TripImageEntity.Create(true, 1, ImageBlobEntity.Create(CreateBlob("img.png"), []));
        var payment = TripPaymentScheduleEntity.Create(2, "Pay", "$1", "P Desc");
        var include = TripPriceIncludeEntity.Create(3, true, "Content");
        var req = TripRequirementEntity.Create(4, "Req");
        var schedule = TripScheduleEntity.Create(5, "Sched", "Sched C");
        var suggested = TripSuggestedFlightEntity.Create(6, ImageBlobEntity.Create(CreateBlob("fl.png"), []));

        entity.GetType().GetProperty("Advantages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(entity, new List<AdvantageEntity> { advantage });
        entity.GetType().GetProperty("Highlights", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(entity, new List<HighlightEntity> { highlight });
        entity.GetType().GetProperty("Images", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(entity, new List<TripImageEntity> { tripImage });
        entity.GetType().GetProperty("PaymentSchedules", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(entity, new List<TripPaymentScheduleEntity> { payment });
        entity.GetType().GetProperty("PriceIncludes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(entity, new List<TripPriceIncludeEntity> { include });
        entity.GetType().GetProperty("Requirements", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(entity, new List<TripRequirementEntity> { req });
        entity.GetType().GetProperty("Schedules", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(entity, new List<TripScheduleEntity> { schedule });
        entity.GetType().GetProperty("SuggestedFlights", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(entity, new List<TripSuggestedFlightEntity> { suggested });

        var vm = TripVM.MapFromDomainEntity((TripEntity)entity);

        Assert.Equal(tripId, vm.Id);
        Assert.Single(vm.Advantages);
        Assert.Single(vm.Highlights);
        Assert.Single(vm.Images);
        Assert.Single(vm.PaymentSchedules);
        Assert.Single(vm.PriceIncludes);
        Assert.Single(vm.Requirements);
        Assert.Single(vm.Schedules);
        Assert.Single(vm.SuggestedFlights);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static BlobBaseEntity CreateBlob(string filename)
    {
        var blob = Activator.CreateInstance(typeof(BlobBaseEntity), true)!;
        blob.GetType().GetProperty("Id")!.SetValue(blob, Guid.NewGuid());
        blob.GetType().GetProperty("Filename")!.SetValue(blob, filename);
        blob.GetType().GetProperty("BlobUrl")!.SetValue(blob, "https://blob");
        blob.GetType().GetProperty("BlobPath")!.SetValue(blob, "/p");
        blob.GetType().GetProperty("Size")!.SetValue(blob, 10L);
        return (BlobBaseEntity)blob;
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}
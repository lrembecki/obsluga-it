using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripSuggestedFlightVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var tripId = Guid.NewGuid();
        var imageId = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(TripSuggestedFlightEntity), true)!;
        Set(entity, "TripId", tripId);
        Set(entity, "Order", 6);

        var image = Activator.CreateInstance(typeof(ImageBlobEntity), true)!;
        Set(image, "Id", imageId);
        Set(image, "Filename", "flight.png");
        Set(image, "BlobUrl", "https://blob");
        Set(image, "BlobPath", "/fl");
        Set(image, "Size", 3L);
        Set(entity, "Image", image);

        var vm = TripSuggestedFlightVM.MapFromDomainEntity((TripSuggestedFlightEntity)entity);

        Assert.Equal(tripId, vm.TripId);
        Assert.Equal(6, vm.Order);
        Assert.Equal(imageId, vm.Image.Id);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripSuggestedFlightVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

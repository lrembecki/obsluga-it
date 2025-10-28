using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripImageVMTests
{
 [Fact]
 public void MapFromDomainEntity_MapsAllFields()
 {
 var tripId = Guid.NewGuid();
 var imageId = Guid.NewGuid();
 var entity = Activator.CreateInstance(typeof(TripImageEntity), true)!;
 Set(entity, "TripId", tripId);
 Set(entity, "IsMain", true);
 Set(entity, "Order",3);

 var image = Activator.CreateInstance(typeof(ImageBlobEntity), true)!;
 Set(image, "Id", imageId);
 Set(image, "Filename", "trip.png");
 Set(image, "BlobUrl", "https://blob");
 Set(image, "BlobPath", "/tp");
 Set(image, "Size",2L);
 Set(entity, "Image", image);

 var vm = TripImageVM.MapFromDomainEntity((TripImageEntity)entity);

 Assert.Equal(tripId, vm.TripId);
 Assert.Equal(3, vm.Order);
 Assert.True(vm.IsMain);
 Assert.Equal(imageId, vm.Image.Id);
 }

 [Fact]
 public void MapFromDomainEntity_Null_ReturnsNull()
 {
 var vm = TripImageVM.MapFromDomainEntity(null!);
 Assert.Null(vm);
 }

 private static void Set(object target, string property, object? value)
 => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

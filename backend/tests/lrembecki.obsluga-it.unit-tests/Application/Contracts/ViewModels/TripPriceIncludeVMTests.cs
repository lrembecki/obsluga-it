using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripPriceIncludeVMTests
{
 [Fact]
 public void MapFromDomainEntity_MapsAllFields()
 {
 var tripId = Guid.NewGuid();
 var entity = Activator.CreateInstance(typeof(TripPriceIncludeEntity), true)!;
 Set(entity, "TripId", tripId);
 Set(entity, "Order",2);
 Set(entity, "Includes", true);
 Set(entity, "Content", "Includes something");

 var vm = TripPriceIncludeVM.MapFromDomainEntity((TripPriceIncludeEntity)entity);

 Assert.Equal(tripId, vm.TripId);
 Assert.Equal(2, vm.Order);
 Assert.True(vm.Includes);
 Assert.Equal("Includes something", vm.Content);
 }

 [Fact]
 public void MapFromDomainEntity_Null_ReturnsNull()
 {
 var vm = TripPriceIncludeVM.MapFromDomainEntity(null!);
 Assert.Null(vm);
 }

 private static void Set(object target, string property, object? value)
 => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

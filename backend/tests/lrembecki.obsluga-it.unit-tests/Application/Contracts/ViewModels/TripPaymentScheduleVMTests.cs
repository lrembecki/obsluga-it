using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripPaymentScheduleVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var tripId = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(TripPaymentScheduleEntity), true)!;
        Set(entity, "TripId", tripId);
        Set(entity, "Order", 1);
        Set(entity, "Title", "Payment1");
        Set(entity, "Price", "$100");
        Set(entity, "Description", "Desc");

        var vm = TripPaymentScheduleVM.MapFromDomainEntity((TripPaymentScheduleEntity)entity);

        Assert.Equal(tripId, vm.TripId);
        Assert.Equal(1, vm.Order);
        Assert.Equal("Payment1", vm.Title);
        Assert.Equal("$100", vm.Price);
        Assert.Equal("Desc", vm.Description);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripPaymentScheduleVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

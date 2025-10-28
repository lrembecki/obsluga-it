using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripScheduleVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var tripId = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(TripScheduleEntity), true)!;
        Set(entity, "TripId", tripId);
        Set(entity, "Order", 5);
        Set(entity, "Title", "Day1");
        Set(entity, "Content", "Activities");

        var vm = TripScheduleVM.MapFromDomainEntity((TripScheduleEntity)entity);

        Assert.Equal(tripId, vm.TripId);
        Assert.Equal(5, vm.Order);
        Assert.Equal("Day1", vm.Title);
        Assert.Equal("Activities", vm.Content);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripScheduleVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

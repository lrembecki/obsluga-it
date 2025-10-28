using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripRequirementVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var tripId = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(TripRequirementEntity), true)!;
        Set(entity, "TripId", tripId);
        Set(entity, "Order", 4);
        Set(entity, "Description", "Requirement");

        var vm = TripRequirementVM.MapFromDomainEntity((TripRequirementEntity)entity);

        Assert.Equal(tripId, vm.TripId);
        Assert.Equal(4, vm.Order);
        Assert.Equal("Requirement", vm.Description);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripRequirementVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

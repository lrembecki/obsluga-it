using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class SubscriptionVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var id = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(SubscriptionEntity), true)!;
        Set(entity, "Id", id);
        Set(entity, "Name", "SubName");

        var vm = SubscriptionVM.MapFromDomainEntity((SubscriptionEntity)entity);

        Assert.Equal(id, vm.Id);
        Assert.Equal("SubName", vm.Name);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

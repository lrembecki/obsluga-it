using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class FileGroupVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var id = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(FileGroupEntity), true)!;
        Set(entity, "Id", id);
        Set(entity, "Name", "Group A");

        var vm = FileGroupVM.MapFromDomainEntity((FileGroupEntity)entity);

        Assert.Equal(id, vm.Id);
        Assert.Equal("Group A", vm.Name);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = FileGroupVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

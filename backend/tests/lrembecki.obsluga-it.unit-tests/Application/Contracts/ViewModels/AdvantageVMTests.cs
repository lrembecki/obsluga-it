using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class AdvantageVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(AdvantageEntity), true)!;
        Set(entity, "Id", id);
        Set(entity, "Title", "Title A");
        Set(entity, "Content", "Content A");

        // Act
        var vm = AdvantageVM.MapFromDomainEntity((AdvantageEntity)entity);

        // Assert
        Assert.Equal(id, vm.Id);
        Assert.Equal("Title A", vm.Title);
        Assert.Equal("Content A", vm.Content);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        // Act
        var vm = AdvantageVM.MapFromDomainEntity(null!);
        // Assert
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

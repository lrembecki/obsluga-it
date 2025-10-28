using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class HighlightVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(HighlightEntity), true)!;
        Set(entity, "Id", id);
        Set(entity, "Title", "Highlight");
        Set(entity, "Icon", "star");

        // Act
        var vm = HighlightVM.MapFromDomainEntity((HighlightEntity)entity);

        // Assert
        Assert.Equal(id, vm.Id);
        Assert.Equal("Highlight", vm.Title);
        Assert.Equal("star", vm.Icon);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = HighlightVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

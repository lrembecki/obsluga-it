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
        var entity = AdvantageEntity.Create(id, "Title A", "Content A");

        // Act
        var vm = AdvantageVM.MapFromDomainEntity(entity);

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
}

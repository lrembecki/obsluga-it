using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class HighlightVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFields()
    {
        var entity = HighlightEntity.Create(Guid.NewGuid(), "Title", "Icon");
        var vm = HighlightVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.Id, vm.Id);
        Assert.Equal(entity.Title, vm.Title);
        Assert.Equal(entity.Icon, vm.Icon);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = HighlightVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }
}

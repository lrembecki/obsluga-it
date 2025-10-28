using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TagVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFields()
    {
        var entity = TagEnity.Create(Guid.NewGuid(), "Tag");
        var vm = TagVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.Id, vm.Id);
        Assert.Equal(entity.Name, vm.Name);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TagVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }
}

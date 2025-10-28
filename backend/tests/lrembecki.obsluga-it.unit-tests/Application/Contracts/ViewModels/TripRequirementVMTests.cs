using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripRequirementVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFields()
    {
        var entity = TripRequirementEntity.Create(1, "Req");
        var vm = TripRequirementVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.TripId, vm.TripId);
        Assert.Equal(entity.Order, vm.Order);
        Assert.Equal(entity.Description, vm.Description);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripRequirementVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }
}

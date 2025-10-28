using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripScheduleVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFields()
    {
        var entity = TripScheduleEntity.Create(1, "Title", "Content");
        var vm = TripScheduleVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.TripId, vm.TripId);
        Assert.Equal(entity.Order, vm.Order);
        Assert.Equal(entity.Title, vm.Title);
        Assert.Equal(entity.Content, vm.Content);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripScheduleVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }
}

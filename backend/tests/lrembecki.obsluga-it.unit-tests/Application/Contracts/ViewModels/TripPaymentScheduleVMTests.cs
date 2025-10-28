using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripPaymentScheduleVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFields()
    {
        var entity = TripPaymentScheduleEntity.Create(1, "Title", "$1", "Desc");
        var vm = TripPaymentScheduleVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.TripId, vm.TripId);
        Assert.Equal(entity.Order, vm.Order);
        Assert.Equal(entity.Title, vm.Title);
        Assert.Equal(entity.Price, vm.Price);
        Assert.Equal(entity.Description, vm.Description);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripPaymentScheduleVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }
}

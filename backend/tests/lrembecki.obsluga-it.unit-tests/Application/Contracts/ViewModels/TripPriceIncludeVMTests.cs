using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripPriceIncludeVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFields()
    {
        var entity = TripPriceIncludeEntity.Create(1, true, "Content");
        var vm = TripPriceIncludeVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.TripId, vm.TripId);
        Assert.Equal(entity.Order, vm.Order);
        Assert.Equal(entity.Includes, vm.Includes);
        Assert.Equal(entity.Content, vm.Content);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripPriceIncludeVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }
}

using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class SubscriptionVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFields()
    {
        var entity = SubscriptionEntity.Create(Guid.NewGuid(), "Sub");
        var vm = SubscriptionVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.Id, vm.Id);
        Assert.Equal(entity.Name, vm.Name);
    }
}

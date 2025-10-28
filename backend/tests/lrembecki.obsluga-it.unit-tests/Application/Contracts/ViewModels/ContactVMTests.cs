using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class ContactVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var id = Guid.NewGuid();
        var entity = ContactEntity.Create(id, "contact@example.com", "234567890");
        var vm = ContactVM.MapFromDomainEntity(entity);

        Assert.Equal(id, vm.Id);
        Assert.Equal("contact@example.com", vm.Email);
        Assert.Equal("234567890", vm.Phone);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = ContactVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }
}

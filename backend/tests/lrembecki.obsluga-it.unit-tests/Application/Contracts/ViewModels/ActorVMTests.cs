using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class ActorVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFieldsAndContacts()
    {
        var actorId = Guid.NewGuid();
        var contactId = Guid.NewGuid();

        var entity = ActorEntity.Create(actorId, "Alice", "Smith");
        var contact = ContactEntity.Create(contactId, "alice@example.com", "123123123");

        entity.Contacts.Add(contact);

        var vm = ActorVM.MapFromDomainEntity(entity);

        Assert.Equal(actorId, vm.Id);
        Assert.Equal("Alice", vm.Firstname);
        Assert.Equal("Smith", vm.Lastname);
        Assert.Single(vm.Contacts);
        var c = vm.Contacts.First();
        Assert.Equal(contactId, c.Id);
        Assert.Equal("alice@example.com", c.Email);
        Assert.Equal("123123123", c.Phone);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = ActorVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }
}

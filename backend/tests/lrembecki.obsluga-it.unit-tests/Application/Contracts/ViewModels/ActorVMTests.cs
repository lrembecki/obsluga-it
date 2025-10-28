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
        var entity = Activator.CreateInstance(typeof(ActorEntity), true)!;
        Set(entity, "Id", actorId);
        Set(entity, "Firstname", "Alice");
        Set(entity, "Lastname", "Smith");

        var contact = Activator.CreateInstance(typeof(ContactEntity), true)!;
        Set(contact, "Id", contactId);
        Set(contact, "Email", new Email("alice@example.com"));
        Set(contact, "Phone", new Phone("123"));

        // add contact via backing field
        var contactsField = typeof(ActorEntity).GetField("_contacts", BindingFlags.Instance | BindingFlags.NonPublic)!;
        var set = (HashSet<ContactEntity>)contactsField.GetValue(entity)!;
        set.Add((ContactEntity)contact);

        var vm = ActorVM.MapFromDomainEntity((ActorEntity)entity);

        Assert.Equal(actorId, vm.Id);
        Assert.Equal("Alice", vm.Firstname);
        Assert.Equal("Smith", vm.Lastname);
        Assert.Single(vm.Contacts);
        var c = vm.Contacts.First();
        Assert.Equal(contactId, c.Id);
        Assert.Equal("alice@example.com", c.Email);
        Assert.Equal("123", c.Phone);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = ActorVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

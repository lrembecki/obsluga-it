using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class ContactVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var id = Guid.NewGuid();
        var entity = Activator.CreateInstance(typeof(ContactEntity), true)!;
        Set(entity, "Id", id);
        Set(entity, "Email", new Email("contact@example.com"));
        Set(entity, "Phone", new Phone("999"));

        var vm = ContactVM.MapFromDomainEntity((ContactEntity)entity);

        Assert.Equal(id, vm.Id);
        Assert.Equal("contact@example.com", vm.Email);
        Assert.Equal("999", vm.Phone);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = ContactVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

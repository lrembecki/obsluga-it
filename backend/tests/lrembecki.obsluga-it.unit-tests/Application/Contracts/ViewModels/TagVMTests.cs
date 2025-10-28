using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TagVMTests
{
 [Fact]
 public void MapFromDomainEntity_MapsAllFields()
 {
 var id = Guid.NewGuid();
 var entity = Activator.CreateInstance(typeof(TagEnity), true)!;
 Set(entity, "Id", id);
 Set(entity, "Name", "Tag1");

 var vm = TagVM.MapFromDomainEntity((TagEnity)entity);

 Assert.Equal(id, vm.Id);
 Assert.Equal("Tag1", vm.Name);
 }

 [Fact]
 public void MapFromDomainEntity_Null_ReturnsNull()
 {
 var vm = TagVM.MapFromDomainEntity(null!);
 Assert.Null(vm);
 }

 private static void Set(object target, string property, object? value)
 => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

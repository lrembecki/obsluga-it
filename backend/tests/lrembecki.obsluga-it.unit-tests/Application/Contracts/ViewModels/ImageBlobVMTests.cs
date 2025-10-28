using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class ImageBlobVMTests
{
 [Fact]
 public void MapFromDomainEntity_MapsAllFields()
 {
 var id = Guid.NewGuid();
 var entity = Activator.CreateInstance(typeof(ImageBlobEntity), true)!;
 Set(entity, "Id", id);
 Set(entity, "Filename", "img.png");
 Set(entity, "BlobUrl", "https://b/u");
 Set(entity, "BlobPath", "/path");
 Set(entity, "Size",123L);
 Set(entity, "DisplayName", "Display");
 Set(entity, "Description", "Desc");
 Set(entity, "Width",800L);
 Set(entity, "Height",600L);

 var vm = ImageBlobVM.MapFromDomainEntity((ImageBlobEntity)entity);

 Assert.Equal(id, vm.Id);
 Assert.Equal("img.png", vm.Filename);
 Assert.Equal("https://b/u", vm.BlobUrl);
 Assert.Equal("/path", vm.BlobPath);
 Assert.Equal(123L, vm.Size);
 Assert.Equal("Display", vm.DisplayName);
 Assert.Equal("Desc", vm.Description);
 Assert.Equal(800L, vm.Width);
 Assert.Equal(600L, vm.Heiht);
 }

 [Fact]
 public void MapFromDomainEntity_Null_ReturnsNull()
 {
 var vm = ImageBlobVM.MapFromDomainEntity(null!);
 Assert.Null(vm);
 }

 private static void Set(object target, string property, object? value)
 => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

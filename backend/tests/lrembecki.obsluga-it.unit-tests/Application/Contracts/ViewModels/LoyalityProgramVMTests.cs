using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class LoyalityProgramVMTests
{
 [Fact]
 public void MapFromDomainEntity_MapsAllFields()
 {
 var id = Guid.NewGuid();
 var imageId = Guid.NewGuid();
 var entity = Activator.CreateInstance(typeof(LoyalityProgramEntity), true)!;
 Set(entity, "Id", id);
 Set(entity, "Title", "Title");
 Set(entity, "Name", "Name");
 Set(entity, "Description", "Description");

 var image = Activator.CreateInstance(typeof(ImageBlobEntity), true)!;
 Set(image, "Id", imageId);
 Set(image, "Filename", "lp.png");
 Set(image, "BlobUrl", "https://blob");
 Set(image, "BlobPath", "/p");
 Set(image, "Size",1L);
 Set(entity, "Image", image);

 var vm = LoyalityProgramVM.MapFromDomainEntity((LoyalityProgramEntity)entity);

 Assert.Equal(id, vm.Id);
 Assert.Equal("Title", vm.Title);
 Assert.Equal("Name", vm.Name);
 Assert.Equal("Description", vm.Description);
 Assert.Equal(imageId, vm.Image.Id);
 }

 [Fact]
 public void MapFromDomainEntity_Null_ReturnsNull()
 {
 var vm = LoyalityProgramVM.MapFromDomainEntity(null!);
 Assert.Null(vm);
 }

 private static void Set(object target, string property, object? value)
 => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

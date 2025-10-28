using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class FileVMTests
{
 [Fact]
 public void Map_MapsAllFields()
 {
 var id = Guid.NewGuid();
 var entity = (Activator.CreateInstance(typeof(FileBlobEntity), true) as FileBlobEntity)!;
 Set(entity, "Id", id);
 Set(entity, "GroupId", Guid.NewGuid());
 Set(entity, "Filename", "file.txt");
 Set(entity, "DisplayName", "Display");
 Set(entity, "Description", "Desc");
 Set(entity, "BlobPath", "/blobpath");
 Set(entity, "BlobUrl", "https://blob");
 Set(entity, "Size",789L);
 Set(entity, "Position",5);

 var vm = FileVM.Map(entity);

 Assert.Equal(id, vm.FileId);
 Assert.Equal(entity.GroupId, vm.FileGroupId);
 Assert.Equal("file.txt", vm.Filename);
 Assert.Equal("Display", vm.DisplayName);
 Assert.Equal("Desc", vm.Description);
 Assert.Equal("/blobpath", vm.BlobPath);
 Assert.Equal("https://blob", vm.BlobUrl);
 Assert.Equal(789L, vm.Size);
 Assert.Equal(5, vm.Position);
 }

 [Fact]
 public void Map_Null_ReturnsNull()
 {
 var vm = FileVM.Map(null!);
 Assert.Null(vm);
 }

 private static void Set(object target, string property, object? value)
 => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

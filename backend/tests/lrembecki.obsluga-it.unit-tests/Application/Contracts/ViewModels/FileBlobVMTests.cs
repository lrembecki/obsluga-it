using System.Reflection;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class FileBlobVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var id = Guid.NewGuid();
        var entity = (Activator.CreateInstance(typeof(FileBlobEntity), true) as FileBlobEntity)!;
        Set(entity, "Id", id);
        Set(entity, "Filename", "file.pdf");
        Set(entity, "BlobUrl", "https://b/u");
        Set(entity, "BlobPath", "/blob");
        Set(entity, "Size", 456L);
        Set(entity, "DisplayName", "File Display");
        Set(entity, "Description", "File Desc");
        Set(entity, "Position", 4);
        Set(entity, "GroupId", Guid.NewGuid());

        var vm = FileBlobVM.MapFromDomainEntity((FileBlobEntity)entity);

        Assert.Equal(id, vm.Id);
        Assert.Equal("file.pdf", vm.Filename);
        Assert.Equal("https://b/u", vm.BlobUrl);
        Assert.Equal("/blob", vm.BlobPath);
        Assert.Equal(456L, vm.Size);
        Assert.Equal("File Display", vm.DisplayName);
        Assert.Equal("File Desc", vm.Description);
        Assert.Equal(4, vm.Postion);
        Assert.Equal(entity.GroupId, vm.GroupId);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = FileBlobVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static void Set(object target, string property, object? value)
    => target.GetType().GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.SetValue(target, value);
}

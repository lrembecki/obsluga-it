using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class FileBlobVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsAllFields()
    {
        var blob = CreateBlob("file.txt");
        var group = FileGroupEntity.Create(Guid.NewGuid(), "Group");
        var entity = FileBlobEntity.Create(Guid.NewGuid(), "Disp", "Desc", 3, group, blob);
        var vm = FileBlobVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.Id, vm.Id);
        Assert.Equal(entity.Filename, vm.Filename);
        Assert.Equal(entity.BlobUrl, vm.BlobUrl);
        Assert.Equal(entity.BlobPath, vm.BlobPath);
        Assert.Equal(entity.Size, vm.Size);
        Assert.Equal(entity.DisplayName, vm.DisplayName);
        Assert.Equal(entity.Description, vm.Description);
        Assert.Equal(entity.Position, vm.Postion);
        Assert.Equal(entity.GroupId, vm.GroupId);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = FileBlobVM.MapFromDomainEntity(null!);
        Assert.Null(vm);
    }

    private static BlobBaseEntity CreateBlob(string filename)
    {
        var blob = Activator.CreateInstance(typeof(BlobBaseEntity), true)!;
        blob.GetType().GetProperty("Id")!.SetValue(blob, Guid.NewGuid());
        blob.GetType().GetProperty("Filename")!.SetValue(blob, filename);
        blob.GetType().GetProperty("BlobUrl")!.SetValue(blob, "https://blob");
        blob.GetType().GetProperty("BlobPath")!.SetValue(blob, "/p");
        blob.GetType().GetProperty("Size")!.SetValue(blob, 10L);
        return (BlobBaseEntity)blob;
    }
}

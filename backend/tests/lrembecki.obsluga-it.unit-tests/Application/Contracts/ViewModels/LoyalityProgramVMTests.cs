using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class LoyalityProgramVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFieldsAndImage()
    {
        var blob = CreateBlob("img.png");
        var image = ImageBlobEntity.Create(blob, []);
        var entity = LoyalityProgramEntity.Create(Guid.NewGuid(), "Name", "Title", "Desc", image);
        var vm = LoyalityProgramVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.Id, vm.Id);
        Assert.Equal(entity.Title, vm.Title);
        Assert.Equal(entity.Name, vm.Name);
        Assert.Equal(entity.Description, vm.Description);
        Assert.Equal(entity.Image.Id, vm.Image.Id);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = LoyalityProgramVM.MapFromDomainEntity(null!);
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

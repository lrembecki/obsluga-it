using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class TripImageVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsFields()
    {
        var blob = CreateBlob("img.png");
        var image = ImageBlobEntity.Create(blob, []);
        var entity = TripImageEntity.Create(true, 1, image);
        var vm = TripImageVM.MapFromDomainEntity(entity);
        Assert.Equal(entity.TripId, vm.TripId);
        Assert.Equal(entity.Order, vm.Order);
        Assert.Equal(entity.IsMain, vm.IsMain);
        Assert.Equal(entity.Image.Id, vm.Image.Id);
    }

    [Fact]
    public void MapFromDomainEntity_Null_ReturnsNull()
    {
        var vm = TripImageVM.MapFromDomainEntity(null!);
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

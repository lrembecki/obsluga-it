using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Enums;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class UnitOfMeasureEntityTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var id = Guid.NewGuid();
        var entity = UnitOfMeasureEntity.Create(id, UnitOfMeasureType.Weight, "Kilogram", "kg", "KGM");
        Assert.Equal(id, entity.Id);
        Assert.Equal(UnitOfMeasureType.Weight, entity.Type);
        Assert.Equal("Kilogram", entity.Name);
        Assert.Equal("kg", entity.ShortName);
        Assert.Equal("KGM", entity.ShortCode);
    }
}

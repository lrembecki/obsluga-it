using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class TripPriceIncludeEntityTests
{
    [Fact]
    public void Create_SetsFields()
    {
        var entity = TripPriceIncludeEntity.Create(1, true, "Content");
        Assert.Equal(1, entity.Order);
        Assert.True(entity.Includes);
        Assert.Equal("Content", entity.Content);
    }

    [Fact]
    public void Update_ModifiesFields()
    {
        var entity = TripPriceIncludeEntity.Create(1, true, "C1");
        entity.Update(2, false, "C2");
        Assert.Equal(2, entity.Order);
        Assert.False(entity.Includes);
        Assert.Equal("C2", entity.Content);
    }
}

using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class TripScheduleEntityTests
{
    [Fact]
    public void Create_SetsFields()
    {
        var entity = TripScheduleEntity.Create(1, "Title", "Content");
        Assert.Equal(1, entity.Order);
        Assert.Equal("Title", entity.Title);
        Assert.Equal("Content", entity.Content);
    }

    [Fact]
    public void Update_ModifiesFields()
    {
        var entity = TripScheduleEntity.Create(1, "T1", "C1");
        entity.Update(2, "T2", "C2");
        Assert.Equal(2, entity.Order);
        Assert.Equal("T2", entity.Title);
        Assert.Equal("C2", entity.Content);
    }
}

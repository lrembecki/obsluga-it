using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class TripPaymentScheduleEntityTests
{
    [Fact]
    public void Create_SetsFields()
    {
        var entity = TripPaymentScheduleEntity.Create(1, "Title", "$10", "Desc");
        Assert.Equal(1, entity.Order);
        Assert.Equal("Title", entity.Title);
        Assert.Equal("$10", entity.Price);
        Assert.Equal("Desc", entity.Description);
    }

    [Fact]
    public void Update_ModifiesFields()
    {
        var entity = TripPaymentScheduleEntity.Create(1, "T1", "$1", "D1");
        entity.Update(2, "T2", "$2", "D2");
        Assert.Equal(2, entity.Order);
        Assert.Equal("T2", entity.Title);
        Assert.Equal("$2", entity.Price);
        Assert.Equal("D2", entity.Description);
    }
}

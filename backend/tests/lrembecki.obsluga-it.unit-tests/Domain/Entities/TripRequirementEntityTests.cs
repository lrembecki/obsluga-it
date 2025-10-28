using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class TripRequirementEntityTests
{
    [Fact]
    public void Create_SetsFields()
    {
        var entity = TripRequirementEntity.Create(1, "Req");
        Assert.Equal(1, entity.Order);
        Assert.Equal("Req", entity.Description);
    }

    [Fact]
    public void Update_ModifiesFields()
    {
        var entity = TripRequirementEntity.Create(1, "R1");
        entity.Update(2, "R2");
        Assert.Equal(2, entity.Order);
        Assert.Equal("R2", entity.Description);
    }
}

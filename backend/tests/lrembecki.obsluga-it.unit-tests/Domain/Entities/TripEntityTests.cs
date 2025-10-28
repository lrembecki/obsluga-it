using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class TripEntityTests
{
    [Fact]
    public void Create_SetsPropertiesAndInitializesCollections()
    {
        var id = Guid.NewGuid();
        var entity = TripEntity.Create(id, "Title", "Sub", "Desc");
        Assert.Equal(id, entity.Id);
        Assert.Equal("Title", entity.Title);
        Assert.Equal("Sub", entity.Subtitle);
        Assert.Equal("Desc", entity.Description);
        Assert.Empty(entity.Advantages);
        Assert.Empty(entity.Highlights);
        Assert.Empty(entity.Images);
        Assert.Empty(entity.PaymentSchedules);
        Assert.Empty(entity.PriceIncludes);
        Assert.Empty(entity.Requirements);
        Assert.Empty(entity.Schedules);
        Assert.Empty(entity.SuggestedFlights);
    }

    [Fact]
    public void Update_ChangesBasicFields()
    {
        var entity = TripEntity.Create(Guid.NewGuid(), "T1", "S1", "D1");
        entity.Update("T2", "S2", "D2");
        Assert.Equal("T2", entity.Title);
        Assert.Equal("S2", entity.Subtitle);
        Assert.Equal("D2", entity.Description);
    }
}

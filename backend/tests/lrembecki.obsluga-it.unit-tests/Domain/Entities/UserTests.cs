using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Create_SetsProperties_AndAddsDomainEvent()
    {
        var id = Guid.NewGuid();
        var email = "alice@example.com";
        var user = UserEntity.Create(id, email);

        Assert.Equal(id, user.Id);
        Assert.Equal(email, user.Email);
        Assert.Empty(user.UserSubscriptions);
        Assert.Single(user.DomainEvents);
        var evt = user.DomainEvents.First();
        Assert.Equal(id, ((dynamic)evt).UserId);
    }
}

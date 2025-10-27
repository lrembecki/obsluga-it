using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Create_SetsProperties_AndAddsDomainEvent()
    {
        var id = Guid.NewGuid();
        var email = new Email("alice@example.com");
        var user = UserEntity.Create(id, email);

        Assert.Equal(id, user.Id);
        Assert.Equal(email, user.Email);
        Assert.Empty(user.UserSubscriptions);
        Assert.Single(user.DomainEvents);
        var evt = user.DomainEvents.First();
        Assert.Equal(id, ((dynamic)evt).UserId);
    }
}

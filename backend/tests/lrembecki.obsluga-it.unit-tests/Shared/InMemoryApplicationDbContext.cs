using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Providers;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.unit_tests.Shared;

internal static class InMemoryApplicationDbContext
{
    public static ApplicationDbContext Create(string? dbName = null, Guid? subscriptionId = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        return new ApplicationDbContext(options, new DateProvider(), FakeSessionAccessor.Dummy(subscriptionId: subscriptionId));
    }
}
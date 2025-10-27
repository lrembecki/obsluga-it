using lrembecki.obsluga_it.infrastructure.Persistence;
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

        return new ApplicationDbContext(options);
    }
}
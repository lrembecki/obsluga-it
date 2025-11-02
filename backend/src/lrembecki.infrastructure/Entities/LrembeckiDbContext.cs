using Microsoft.EntityFrameworkCore;

namespace lrembecki.infrastructure.Entities;

internal class ObslugaItDbContext(DbContextOptions<ObslugaItDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ObslugaItDbContext).Assembly);
        modelBuilder.HasDefaultSchema("app");
    }
}

namespace lrembecki.obsluga_it.infrastructure;

using Microsoft.EntityFrameworkCore;

internal class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options)
{
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
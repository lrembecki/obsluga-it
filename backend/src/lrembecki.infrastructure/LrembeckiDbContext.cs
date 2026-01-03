using Microsoft.EntityFrameworkCore;

namespace lrembecki.infrastructure;

internal class ObslugaItDbContext(DbContextOptions<ObslugaItDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ObslugaItDbContext).Assembly);
        modelBuilder.HasDefaultSchema("app");

        modelBuilder.ApplyConfigurationFromAccount();
        modelBuilder.ApplyConfigurationFromForms();
        modelBuilder.ApplyConfigurationFromSecurity();
        modelBuilder.ApplyConfigurationFromShared();
        modelBuilder.ApplyConfigurationFromStorage();
        modelBuilder.ApplyConfigurationFromSettings();
        modelBuilder.ApplyConfigurationFromTrotamundos();
        // Add other module configurations here
    }
}

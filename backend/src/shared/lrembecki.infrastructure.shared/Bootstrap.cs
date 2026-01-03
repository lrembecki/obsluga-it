using lrembecki.core.shared.DomainEvents;

using Microsoft.EntityFrameworkCore;

public static class BootstrapShared
{
    public static ModelBuilder ApplyConfigurationFromShared(this ModelBuilder modelBuilder)
    {
        var sharedAssembly = typeof(BootstrapShared).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(sharedAssembly);

        modelBuilder.Ignore<DomainEventEntity>();

        return modelBuilder;
    }
}

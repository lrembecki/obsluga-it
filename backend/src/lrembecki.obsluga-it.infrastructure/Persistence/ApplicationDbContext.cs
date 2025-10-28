namespace lrembecki.obsluga_it.infrastructure.Persistence;

using lrembecki.obsluga_it.application.Abstractions.Factories;
using lrembecki.obsluga_it.application.Abstractions.Providers;
using lrembecki.obsluga_it.domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

internal class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("oit");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateMetadata();

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateMetadata();

        return base.SaveChanges();
    }

    private void UpdateMetadata()
    {
        var entities = ChangeTracker.Entries().ToList();

        var now = this.GetService<IDateProvider>().UtcNow;
        var session = this.GetService<ISessionAccessor>();

        foreach (var entity in entities)
        {
            if (entity.Entity is IHasSubscriptionId subscriptionEntity)
            {
                subscriptionEntity.GetType().GetProperty(nameof(IHasSubscriptionId.SubscriptionId))!.SetValue(entity.Entity, session.SubscriptionId);
            }

            if (entity.Entity is IHasAuditFields auditableEntity && session.UserId is not null)
            {
                switch (entity.State)
                {
                    case EntityState.Added:

                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.CreatedById))!.SetValue(entity.Entity, session.SubscriptionId);
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.CreatedAt))!.SetValue(entity.Entity, now);
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.UpdatedById))!.SetValue(entity.Entity, session.SubscriptionId);
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.UpdatedAt))!.SetValue(entity.Entity, now);
                        break;
                    case EntityState.Modified:
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.UpdatedById))!.SetValue(entity.Entity, session.SubscriptionId);
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.UpdatedAt))!.SetValue(entity.Entity, now);
                        break;
                }
            }
        }
    }
}
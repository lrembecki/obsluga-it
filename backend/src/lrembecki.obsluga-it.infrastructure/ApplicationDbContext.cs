namespace lrembecki.obsluga_it.infrastructure;

using lrembecki.obsluga_it.application.Abstractions.Factories;
using lrembecki.obsluga_it.application.Abstractions.Providers;
using lrembecki.obsluga_it.domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

internal class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDateProvider dateProvider,
    ISessionAccessor sessionFactory)
    : DbContext(options)
{

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var subscriptionId = sessionFactory.SubscriptionId;
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
        var now = dateProvider.UtcNow;

        foreach (var entity in entities)
        {
            if (entity.Entity is IHasSubscriptionId subscriptionEntity)
            {
                subscriptionEntity.GetType().GetProperty(nameof(IHasSubscriptionId.SubscriptionId))!.SetValue(entity.Entity, sessionFactory.SubscriptionId);
            }

            if (entity.Entity is IHasAuditFields auditableEntity && sessionFactory.UserId is not null)
            {
                switch (entity.State)
                {
                    case EntityState.Added:

                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.CreatedById))!.SetValue(entity.Entity, sessionFactory.SubscriptionId);
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.CreatedAt))!.SetValue(entity.Entity, now);
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.UpdatedById))!.SetValue(entity.Entity, sessionFactory.SubscriptionId);
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.UpdatedAt))!.SetValue(entity.Entity, now);
                        break;
                    case EntityState.Modified:
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.UpdatedById))!.SetValue(entity.Entity, sessionFactory.SubscriptionId);
                        auditableEntity.GetType().GetProperty(nameof(IHasAuditFields.UpdatedAt))!.SetValue(entity.Entity, now);
                        break;
                }
            }
        }
    }
}
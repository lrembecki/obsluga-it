using lrembecki.shared.application.Abstractions;
using lrembecki.shared.domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.obsluga_it.infrastructure.Persistence;

internal class EfUnitOfWork(
    IServiceProvider serviceProvider,
    ApplicationDbContext dbContext) : IUnitOfWork
{
    public ApplicationDbContext DbContext => dbContext;

    public IRepository<T> GetRepository<T>() where T : class
        => serviceProvider.GetRequiredService<IRepository<T>>();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateMetadata();

        return dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateMetadata()
    {
        var entities = dbContext.ChangeTracker.Entries().ToList();

        var now = serviceProvider.GetRequiredService<IDateProvider>().UtcNow;
        var session = serviceProvider.GetRequiredService<ISessionAccessor>();

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

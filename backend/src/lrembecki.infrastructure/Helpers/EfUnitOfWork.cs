using lrembecki.core.Entities;
using lrembecki.core.Helpers;
using lrembecki.core.Markers;
using lrembecki.core.Services;
using lrembecki.infrastructure.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.infrastructure.Helpers;

internal class EfUnitOfWork(
    IServiceProvider serviceProvider,
    ObslugaItDbContext dbContext) : IUnitOfWork
{
    public ObslugaItDbContext DbContext => dbContext;
    private IDbContextTransaction _dbContextTransaction = null!;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _dbContextTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContextTransaction != null)
        {
            await _dbContextTransaction.CommitAsync(cancellationToken);
            _dbContextTransaction.Dispose();
            _dbContextTransaction = null!;
        }
    }

    public IRepository<T> GetRepository<T>() where T : class
        => serviceProvider.GetRequiredService<IRepository<T>>();

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_dbContextTransaction != null)
        {
            await _dbContextTransaction.RollbackAsync(cancellationToken);
            _dbContextTransaction.Dispose();
            _dbContextTransaction = null!;
        }
    }

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
        var notifier = serviceProvider.GetRequiredService<INotifier>();

        foreach (var entity in entities)
        {
            if (entity.Entity is BaseEntity baseEntity && baseEntity.DomainEvents.Count > 0)
            {
                baseEntity.DomainEvents.ToList().ForEach(de => notifier.Notify(de with
                {
                    SubscriptionId = session.SubscriptionId!.Value
                }));
            }

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

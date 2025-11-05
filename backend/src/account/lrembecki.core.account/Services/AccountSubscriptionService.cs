using lrembecki.core.account.Dtos;
using lrembecki.core.account.Entities;
using lrembecki.core.account.ViewModels;
using lrembecki.core.Helpers;
using lrembecki.core.Services;

namespace lrembecki.core.account.Services;

public interface IAccountSubscriptionService : ICrudService<AccountSubscriptionDto, AccountSubscriptionVM>;
internal class AccountSubscriptionService(IUnitOfWork uow, ISessionAccessor session) 
    : BaseCrudService<AccountSubscriptionEntity, AccountSubscriptionVM, AccountSubscriptionDto>(uow), IAccountSubscriptionService
{
    private readonly IRepository<PermissionGroupEntity> _permissionGroups = uow.GetRepository<PermissionGroupEntity>();

    protected override IQueryable<AccountSubscriptionEntity> GetAll(IQueryable<AccountSubscriptionEntity> query)
    {
        query = base.GetAll(query);

        query = query.Where(e => e.SubscriptionId == session.SubscriptionId);

        return query;
    }

    protected override async Task<AccountSubscriptionEntity> CreateEntity(Guid id, AccountSubscriptionDto model, CancellationToken cancellationToken)
    {
        var entity = await  base.CreateEntity(id, model, cancellationToken);

        var toAdd = model.PermissionGroups
            .Where(e => !entity.PermissionGroups.Select(y => y.Id).Contains(e))
            .ToList();

        if (toAdd.Count > 0)
        {
            entity.PermissionGroups.AddRange(
                [.. _permissionGroups.GetAll(e => toAdd.Contains(e.Id)) ]
            );
        }

        entity.PermissionGroups.Where(e => !model.PermissionGroups.Contains(e.Id))
            .ToList().ForEach(e => entity.PermissionGroups.Remove(e));

        return entity;
    }

    protected override async Task UpdateEntity(AccountSubscriptionEntity entity, AccountSubscriptionDto model)
    {

        await base.UpdateEntity(entity, model);

        var toAdd = model.PermissionGroups
            .Where(e => !entity.PermissionGroups.Select(y => y.Id).Contains(e))
            .ToList();

        if (toAdd.Count > 0)
        {
            entity.PermissionGroups.AddRange(
                [.. _permissionGroups.GetAll(e => toAdd.Contains(e.Id))]
            );
        }

        entity.PermissionGroups.Where(e => !model.PermissionGroups.Contains(e.Id))
            .ToList().ForEach(e => entity.PermissionGroups.Remove(e));
    }
}

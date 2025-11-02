using lrembecki.core.account.Dtos;
using lrembecki.core.account.Entities;
using lrembecki.core.account.ViewModels;
using lrembecki.core.Services;

namespace lrembecki.core.account.Services;

public interface IAccountSubscriptionService : ICrudService<AccountSubscriptionDto, AccountSubscriptionVM>;
internal class AccountSubscriptionService(IUnitOfWork uow) 
    : BaseCrudService<AccountSubscriptionEntity, AccountSubscriptionVM, AccountSubscriptionDto>(uow), IAccountSubscriptionService
{
    private readonly IRepository<PermissionGroupEntity> _permissionGroups = uow.GetRepository<PermissionGroupEntity>();

    protected override async Task UpdateEntity(AccountSubscriptionEntity entity, AccountSubscriptionDto model)
    {

        await base.UpdateEntity(entity, model);

        entity.PermissionGroups.Clear();

        if (entity.PermissionGroups.Count > 0)
        {
            entity.PermissionGroups.AddRange(
                await _permissionGroups.GetAsync(e => model.PermissionGroups.Contains(e.Id))
            );
        }
    }
}

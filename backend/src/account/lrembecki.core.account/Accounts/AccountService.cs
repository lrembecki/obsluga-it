using lrembecki.core.account.PermissionGroups;
using lrembecki.core.Services;

namespace lrembecki.core.account.Accounts;

public interface IAccountService : ICrudService<AccountDto, AccountVM>;
internal class AccountService(IUnitOfWork uow) : BaseCrudService<AccountEntity, AccountVM, AccountDto>(uow), IAccountService
{
    private readonly IRepository<PermissionGroupEntity> _permissionGroups = uow.GetRepository<PermissionGroupEntity>();

    protected override async Task UpdateEntity(AccountEntity entity, AccountDto model)
    {
        await  base.UpdateEntity(entity, model);

        entity.PermissionGroups.Clear();
    
        if (entity.PermissionGroups.Count > 0)
        {
            entity.PermissionGroups.AddRange(
                await _permissionGroups.GetAsync(e => model.PermissionGroups.Contains(e.Id))
            );
        }
    }
}

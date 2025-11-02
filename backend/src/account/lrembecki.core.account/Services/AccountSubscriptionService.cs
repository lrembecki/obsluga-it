using lrembecki.core.account.Dtos;
using lrembecki.core.account.Entities;
using lrembecki.core.account.ViewModels;
using lrembecki.core.Services;

namespace lrembecki.core.account.Services;

public interface IAccountSubscriptionService : ICrudService<AccountSubscriptionDto, AccountSubscriptionVM>;
internal class AccountSubscriptionService(IUnitOfWork uow) 
    : BaseCrudService<AccountSubscriptionEntity, AccountSubscriptionVM, AccountSubscriptionDto>(uow), IAccountSubscriptionService
{
    protected override Task UpdateEntity(AccountSubscriptionEntity entity, AccountSubscriptionDto model)
    {
        return base.UpdateEntity(entity, model);
    }
}

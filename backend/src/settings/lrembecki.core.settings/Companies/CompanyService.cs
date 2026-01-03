using lrembecki.core.Services;
using lrembecki.core.settings.Address;
using lrembecki.core.settings.Contacts;

namespace lrembecki.core.settings.Companies;

public interface ICompanyService : ICrudService<CompanyDto, CompanyVM>;
sealed class CompanyService(IUnitOfWork uow, IAddressService address) : BaseCrudService<CompanyEntity, CompanyVM, CompanyDto>(uow), ICompanyService
{
    private IRepository<ContactEntity> _contacts => _uow.GetRepository<ContactEntity>();

    protected override async Task<CompanyEntity> CreateEntity(Guid id, CompanyDto model, CancellationToken cancellationToken)
    {
        model = model with { AddressId = (await address.CreateAsync(model.Address, cancellationToken)).Id };

        var entity = await base.CreateEntity(id, model, cancellationToken);

        if (model.PhoneContact.Count > 0)
        {
            entity.PhoneContact.AddRange(_contacts.GetAll(e => model.PhoneContact.Contains(e.Id)).ToList());
        }

        if (model.EmailContact.Count > 0)
        {
            entity.EmailContact.AddRange(_contacts.GetAll(e => model.EmailContact.Contains(e.Id)).ToList());
        }

        return entity;
    }

    protected override async Task UpdateEntity(CompanyEntity entity, CompanyDto model)
    {
        await address.UpdateAsync(entity.AddressId, model.Address);

        entity.PhoneContact.Clear();
        entity.EmailContact.Clear();

        if (model.PhoneContact.Count > 0)
        {
            entity.PhoneContact.AddRange(_contacts.GetAll(e => model.PhoneContact.Contains(e.Id)).ToList());
        }

        if (model.EmailContact.Count > 0)
        {
            entity.EmailContact.AddRange(_contacts.GetAll(e => model.EmailContact.Contains(e.Id)).ToList());
        }


        await base.UpdateEntity(entity, model);
    }
}
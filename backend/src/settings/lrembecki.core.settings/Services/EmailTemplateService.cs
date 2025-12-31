using lrembecki.core.Services;
using lrembecki.core.settings.Dtos;
using lrembecki.core.settings.Entities;
using lrembecki.core.settings.ViewModels;
using lrembecki.core.storage.Services;

namespace lrembecki.core.settings.Services;

public interface IEmailTemplateService : ICrudService<EmailTemplateDto, EmailTemplateVM>;
internal sealed class EmailTemplateService(
    IUnitOfWork uow,
    IStorageService storage
) : BaseCrudService<EmailTemplateEntity, EmailTemplateVM, EmailTemplateDto>(uow), IEmailTemplateService
{
    private IRepository<ContactEntity> _contacts => uow.GetRepository<ContactEntity>();

    protected override async Task<EmailTemplateEntity> CreateEntity(Guid id, EmailTemplateDto model, CancellationToken ct)
    {
        model = model with
        {
            TemplateHtmlId = (await storage.CreateAsync(model.TemplateHtml, ct)).Id
        };

        var entity = await base.CreateEntity(id, model, ct);

        await UpdateContacts(entity, model);

        return entity;
    }

    private async Task UpdateContacts(EmailTemplateEntity entity, EmailTemplateDto model)
    {
        entity.Contacts_to.Clear();
        entity.Contacts_to.AddRange([.. _contacts.GetAll(e => model.Contacts_to.Contains(e.Id))]);
        
        entity.Contacts_cc.Clear();
        entity.Contacts_cc.AddRange([.. _contacts.GetAll(e => model.Contacts_cc.Contains(e.Id))]);

        entity.Contacts_bcc.Clear();
        entity.Contacts_bcc.AddRange([.. _contacts.GetAll(e => model.Contacts_bcc.Contains(e.Id))]);
    }

    protected override async Task UpdateEntity(EmailTemplateEntity entity, EmailTemplateDto model)
    {
        await storage.UpdateAsync(entity.TemplateHtmlId, model.TemplateHtml);

        await UpdateContacts(entity, model);

        await base.UpdateEntity(entity, model);
    }
}

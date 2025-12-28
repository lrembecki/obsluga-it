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
    protected override async Task<EmailTemplateEntity> CreateEntity(Guid id, EmailTemplateDto model, CancellationToken ct)
    {
        model = model with
        {
            TemplateHtmlId = (await storage.CreateAsync(model.TemplateHtml, ct)).Id
        };

        return await base.CreateEntity(id, model, ct);
    }

    protected override async Task UpdateEntity(EmailTemplateEntity entity, EmailTemplateDto model)
    {
        await storage.UpdateAsync(entity.TemplateHtmlId, model.TemplateHtml);
        await base.UpdateEntity(entity, model);
    }
}

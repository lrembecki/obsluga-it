using lrembecki.core.Services;
using lrembecki.core.settings.Dtos;
using lrembecki.core.settings.Entities;
using lrembecki.core.settings.ViewModels;

namespace lrembecki.core.settings.Services;

internal sealed class EmailService(IUnitOfWork uow) : BaseCrudService<EmailEntity, EmailWithPasswordVM, EmailDto>(uow), IEmailService 
{
    protected override async Task UpdateEntity(EmailEntity entity, EmailDto model)
    {
        if (string.IsNullOrEmpty(model.SmtpPassword))
        {
            model = model with
            {
                SmtpPassword = entity.SmtpPassword
            };
        }
        await base.UpdateEntity(entity, model);
    }

    public override async Task<List<EmailWithPasswordVM>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await  base.GetAllAsync(cancellationToken);
        return result.Select(e => e with { SmtpPassword = null! }).ToList();
    }
}

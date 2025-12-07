using lrembecki.core.Services;
using lrembecki.core.settings.Dtos;
using lrembecki.core.settings.Entities;
using lrembecki.core.settings.ViewModels;

namespace lrembecki.core.settings.Services;

public sealed class EmailService(IUnitOfWork uow) : BaseCrudService<EmailEntity, EmailVM, EmailDto>(uow), IEmailService 
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
}

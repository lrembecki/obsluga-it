using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.website.Settings;

public record SettingsSyncCommand(SettingsDto Model) : IRequest<SettingsVM>
{
    internal sealed class SettingsSyncCommandHandler(IUnitOfWork uow, IStorageService storage) : IHandler<SettingsSyncCommand, SettingsVM>
    {
        public async Task<SettingsVM> Handle(SettingsSyncCommand command, CancellationToken ct = default)
        {
            var model = await command.Model.SyncStorageAsync(storage, ct);

            var entity = uow.GetRepository<SettingsEntity>().GetAll().FirstOrDefault()
                ?? SettingsEntity.Create(Guid.NewGuid(), command.Model);

            entity.Update(model);

            await uow.SaveChangesAsync(ct);

            return SettingsVM.Map(entity);
        }
    }
}

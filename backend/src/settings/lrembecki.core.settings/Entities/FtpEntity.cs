using lrembecki.core.Entities;
using lrembecki.core.Markers;
using lrembecki.core.settings.Dtos;

namespace lrembecki.core.settings.Entities;

public class FtpEntity : SubscriptionBaseEntity, IHasId<Guid>, IHasActive
{
    public Guid Id { get; private set; }
    public bool IsActive { get; private set; }
    public string Server { get; private set; } = string.Empty;
    public int Port { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public string RemotePath { get; private set; } = string.Empty;
    public bool UseSsl { get; private set; }
    public static FtpEntity Create(Guid id, FtpDto model)
    {
        var entity = new FtpEntity
        {
            Id = id
        };
        entity.Update(model);
        return entity;

    }

    public void Update(FtpDto model)
    {
        IsActive = model.IsActive;
        Server = model.Server;
        Port = model.Port;
        Username = model.Username;
        Password = model.Password;
        RemotePath = model.RemotePath;
        UseSsl = model.UseSsl;
    }
}

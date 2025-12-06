namespace lrembecki.core.settings.Dtos;

public record FtpDto
{
    public bool IsActive { get; init; }
    public string Server { get; init; } = string.Empty;
    public int Port { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string RemotePath { get; init; } = string.Empty;
    public bool UseSsl { get; init; }
}

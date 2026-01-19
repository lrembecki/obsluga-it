using System.Net.NetworkInformation;

namespace lrembecki.core;

public record ServiceCallResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    public static ServiceCallResult CreateSuccess() => new() { Success = true };
}

public record ServiceCallResult<T> : ServiceCallResult
{
    public T Data { get; set; } = default!;
}

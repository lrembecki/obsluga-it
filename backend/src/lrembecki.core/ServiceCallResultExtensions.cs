using lrembecki.core.Exceptions;

namespace lrembecki.core;

public static class ServiceCallResultExtensions
{
    public static Task<ServiceCallResult<T>> ToServiceCallResultAsync<T>(this Task<T> data)
        => data.ContinueWith(t => t.Result.ToServiceCallResult());

    public static ServiceCallResult<T> ToServiceCallResult<T>(this T data)
        => new()
        {
            Success = true,
            Data = data
        };

    public static ServiceCallResult ToServiceCallResult(this Exception exception)
        => new()
        {
            Success = false,
            ErrorMessage = exception.Message
        };

    public static ServiceCallResult<T> ToServiceCallResult<T>(this Exception exception, T data)
        => new()
        {
            Success = false,
            ErrorMessage = exception.Message
        };

    public static ServiceCallResult<Dictionary<string, string>> ToServiceCallResult(this ValidationException ex)
        => new ServiceCallResult<Dictionary<string, string>>
        {
            Success = false,
            ErrorMessage = ex.Message,
            Data = ex.Model
        };
}
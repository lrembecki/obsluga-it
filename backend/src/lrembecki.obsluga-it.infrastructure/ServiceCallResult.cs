namespace lrembecki.obsluga_it.infrastructure;

public record ServiceCallResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    public static ServiceCallResult CreateExceptionResult(Exception exception)
        => new ServiceCallResult
        {
            Success = false,
            ErrorMessage = exception.Message
        };

    public static ServiceCallResult CreateSuccessResult(string? errorMessage = null)
        => new ServiceCallResult
        {
            Success = true,
            ErrorMessage = errorMessage
        };

    public static ServiceCallResult<T> CreateSuccessResult<T>(T result, string? errorMessage = null)
        => new ServiceCallResult<T>
        {
            Success = true,
            ErrorMessage = errorMessage,
            Result = result
        };
}

public record ServiceCallResult<T> : ServiceCallResult
{
    public T Result { get; set; } = default!;
}

namespace lrembecki.core;

public static class ServiceCallResultExtension
{
    public static ServiceCallResult<T> ToServiceCallResult<T>(this T result)
        => ServiceCallResult.CreateSuccessResult(result);
}

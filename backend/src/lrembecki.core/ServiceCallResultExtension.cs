using lrembecki.core.Exceptions;

namespace lrembecki.core
{
    public static class ServiceCallResultExtension
    {
        public static async Task<ServiceCallResult> ToServiceCallResult(this Task result)
        {
            await result;
            return ServiceCallResult.CreateSuccessResult();
        }


        public static ServiceCallResult<T> ToServiceCallResult<T>(this T result)
            => ServiceCallResult.CreateSuccessResult(result);

        public static ServiceCallResult<Dictionary<string, string>> ToFailServiceCallResult(this ValidationException ex)
            => ServiceCallResult.CreateFailureResult(ex, ex.Model);
    }
}

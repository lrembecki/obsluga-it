using lrembecki.core;
using lrembecki.core.Helpers;
using lrembecki.core.Services;

namespace lrembecki.host.Infrastructure;

public static class GlobalExceptionHandler
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            var uow = context.RequestServices.GetRequiredService<IUnitOfWork>();
            var publisher = context.RequestServices.GetRequiredService<INotifier>();

            try
            {
                await uow.BeginTransactionAsync();

                await next();

                await uow.CommitTransactionAsync();
                
                await publisher.PublishAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackTransactionAsync();

                var result = ServiceCallResult.CreateExceptionResult(ex);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    ArgumentNullException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                await context.Response.WriteAsJsonAsync(result);
            }
        });
        return app;
    }
}

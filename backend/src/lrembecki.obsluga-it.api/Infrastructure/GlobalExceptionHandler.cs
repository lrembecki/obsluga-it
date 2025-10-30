using lrembecki.obsluga_it.infrastructure;

namespace lrembecki.obsluga_it.api.Infrastructure;

public static class GlobalExceptionHandler
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
         {
             try
             {
                 await next();
             }
             catch (Exception ex)
             {
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

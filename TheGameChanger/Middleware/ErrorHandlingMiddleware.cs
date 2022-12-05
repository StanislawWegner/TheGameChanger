using TheGameChanger.Exceptions;

namespace TheGameChanger.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException exceptionNotFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(exceptionNotFound.Message);

            }
            catch
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("unhandled error");
            }
        }
    }
}

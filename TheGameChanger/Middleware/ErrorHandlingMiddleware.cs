using Newtonsoft.Json;
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
                var result = JsonConvert.SerializeObject(new {error = exceptionNotFound.Message});
                context.Response.StatusCode = 404;

                await context.Response.WriteAsync(result);

            }
            catch(DataExistsException exceptionDataExist)
            {
                context.Response.StatusCode = 409;
                var result = JsonConvert.SerializeObject(new { error = exceptionDataExist.Message});
                await context.Response.WriteAsync(result);
            }
            catch
            {
                context.Response.StatusCode = 500;
                var result = JsonConvert.SerializeObject(new { error = "unhandled Exception" });
                await context.Response.WriteAsync(result);
            }
        }
    }
}

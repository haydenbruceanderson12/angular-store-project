using System.Net;
using System.Text.Json;
using API.RequestHelpers;

namespace API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ExceptionHandlingResponse(context.Response.StatusCode,
                exception.Message, exception.StackTrace ?? "Internal Server Error");
                
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}

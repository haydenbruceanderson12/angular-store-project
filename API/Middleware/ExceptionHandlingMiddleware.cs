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
            Console.WriteLine(exception.Message);
        }
    }
}

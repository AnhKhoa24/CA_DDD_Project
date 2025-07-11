using System.Net;
using System.Text.Json;

namespace Api.Middleware;

public class ErrorHandingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandingMiddleware(RequestDelegate next)
    {
        _next = next;                               
    }                                          
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = ex.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result); 
    }
}
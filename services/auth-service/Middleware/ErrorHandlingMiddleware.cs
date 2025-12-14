using System.Net;
using System.Text.Json;
using AuthService.Logging;

namespace AuthService.Middleware;

/// <summary>
/// Middleware для обработки ошибок
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogUnhandledException(_logger, ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = "An error occurred while processing your request." });

        if (exception is ArgumentException || exception is InvalidOperationException)
        {
            code = HttpStatusCode.BadRequest;
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }
        else if (exception is UnauthorizedAccessException)
        {
            code = HttpStatusCode.Unauthorized;
            result = JsonSerializer.Serialize(new { error = "Unauthorized access." });
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}

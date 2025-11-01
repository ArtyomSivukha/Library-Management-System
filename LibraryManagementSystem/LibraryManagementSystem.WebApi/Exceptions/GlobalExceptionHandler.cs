using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception, "Unhandled exception occured");

        httpContext.Response.StatusCode = exception switch
        {
            ApplicationException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };

        await httpContext.Response.WriteAsJsonAsync(
            new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An error occurred.",
                Detail = exception.Message,
            });
          

        return true;
    }
}
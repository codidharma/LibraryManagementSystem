using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Middleware;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;
    private readonly IProblemDetailsService _problemDetailsService;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception occured.");

        ProblemDetails problemDetails = new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section--6.6.1",
            Title = "Internal Server Error"
        };

        ProblemDetailsContext problemDetailsContext = new()
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails
        };
        return await _problemDetailsService.TryWriteAsync(problemDetailsContext);

    }
}

using LMS.Common.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Middleware;

internal sealed class TrackingIdVerifierMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger _logger;

    public TrackingIdVerifierMiddleware(
        IProblemDetailsService problemDetailsService,
        RequestDelegate next,
        ILogger<TrackingIdVerifierMiddleware> logger)
    {
        _next = next;
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Started the execution of the {Name} middleware.", nameof(TrackingIdVerifierMiddleware));

        Endpoint endpoint = context.GetEndpoint();

        _logger.LogInformation("The endpoint is {Name}", endpoint?.DisplayName);

        string trackingId = context.Request.Headers["tracking-id"];


        if (!Guid.TryParse(trackingId, out _))
        {
            string errorMessage = "The tracking-id header is a mandatory header and must be a valid uuid.";

            ProblemDetails problemDetails = new()
            {
                Title = "Generic.BadRequest",
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = errorMessage
            };
            LmsException exception = new(errorMessage);

            ProblemDetailsContext problemDetailsContext = new()
            {
                HttpContext = context,
                Exception = exception,
                ProblemDetails = problemDetails
            };

            await _problemDetailsService.TryWriteAsync(problemDetailsContext);
            _logger.LogInformation("Finished the execution of the {Name} middleware.Failed to verify tracking-id header.", nameof(TrackingIdVerifierMiddleware));
            return;
        }
        _logger.LogInformation("Finished the execution of the {Name} middleware.", nameof(TrackingIdVerifierMiddleware));
        //Invoke next middleware in the pipeline
        await _next(context);

    }
}

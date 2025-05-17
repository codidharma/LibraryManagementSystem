using Serilog.Context;

namespace LMS.Api.Middleware;

internal sealed class LoggingContextTrackingIdEnricherMiddleware
{
    private readonly RequestDelegate _next;
    public LoggingContextTrackingIdEnricherMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        string trackingId = context.Request.Headers[HeadersConstants.TrackingIdHeaderName];

        using (LogContext.PushProperty("TrackingId", trackingId))
        {
            await _next.Invoke(context);
        }
    }
}

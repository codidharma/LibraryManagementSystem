using System.Diagnostics;

namespace LMS.Api.Middleware;

internal sealed class TraceIdStamperMiddleware
{
    private readonly RequestDelegate _next;
    public TraceIdStamperMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        string traceId = Activity.Current?.TraceId.ToString();
        context.Response.Headers.Append("TraceId", traceId);
        await _next.Invoke(context);

    }
}

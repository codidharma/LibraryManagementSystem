namespace LMS.Api.Middleware;

internal static class MiddlewareExtensions
{
    public static IApplicationBuilder UseTrackingIdVerifier(this IApplicationBuilder builder)
    {
        return builder.UseWhen(
            context => !context.Request.Path.IsRequestInvokedForApiDocumentation(),
            builder => builder.UseMiddleware<TrackingIdVerifierMiddleware>());
    }

    public static IApplicationBuilder UseLoggerEnricher(this IApplicationBuilder builder)
    {
        return builder.UseWhen(
            context => !context.Request.Path.IsRequestInvokedForApiDocumentation(),
            builder => builder.UseMiddleware<LoggingContextTrackingIdEnricherMiddleware>());
    }


    public static IApplicationBuilder UseTraceIdStamper(this IApplicationBuilder builder)
    {
        return builder.UseWhen(
            context => !context.Request.Path.IsRequestInvokedForApiDocumentation(),
            builder => builder.UseMiddleware<TraceIdStamperMiddleware>());
    }

    private static bool IsRequestInvokedForApiDocumentation(this PathString pathString)
    {
        const string ScalarUiPathBlurb = "/scalar";
        const string OpenApiPathBlurb = "/openapi";

        return pathString.StartsWithSegments(ScalarUiPathBlurb) || pathString.StartsWithSegments(OpenApiPathBlurb);
    }
}

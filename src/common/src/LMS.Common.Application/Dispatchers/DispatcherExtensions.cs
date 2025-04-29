using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Dispatchers;

public static class DispatcherExtensions
{
    public static void AddDispatchers(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
    }
}

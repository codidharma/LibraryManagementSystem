using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Dispatchers;

public static class DispatcherExtensions
{
    public static void AddDispatchers(this IServiceCollection services)
    {
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped(sp =>
        {
            CommandDispatcher nextDispatcher = new CommandDispatcher(sp);
            ICommandDispatcher decorator = new CommandValidationDecoratorCommandDispatcher(nextDispatcher, sp);
            return decorator;

        });
    }
}

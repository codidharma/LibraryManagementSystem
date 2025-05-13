using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Dispatchers;

public static class DispatcherExtensions
{
    public static void AddDispatchers(this IServiceCollection services)
    {
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddScoped(sp =>
        {
            CommandDispatcher coreDispatcher = new CommandDispatcher(sp);
            CommandValidationDecoratorCommandDispatcher validationDispatcher = new(coreDispatcher, sp);

            ICommandDispatcher decorator = new LoggingDecoratorCommandDispatcher(validationDispatcher, sp);
            return decorator;

        });
    }
}

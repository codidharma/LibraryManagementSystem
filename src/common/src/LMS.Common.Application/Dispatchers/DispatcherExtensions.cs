using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Application.Dispatchers.DomainEventDispatcher;
using LMS.Common.Application.Dispatchers.Query;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Dispatchers;

public static class DispatcherExtensions
{
    public static void AddDispatchers(this IServiceCollection services)
    {
        services.AddScoped(sp =>
        {
            CommandDispatcher coreDispatcher = new CommandDispatcher(sp);
            CommandValidationDecoratorCommandDispatcher validationDispatcher = new(coreDispatcher, sp);

            ICommandDispatcher decorator = new LoggingDecoratorCommandDispatcher(validationDispatcher, sp);
            return decorator;

        });
        services.AddScoped(sp =>
        {
            QueryDispatcher coreDispatcher = new(sp);
            IQueryDispatcher decorator = new LoggingDecoratorQueryDispatcher(coreDispatcher, sp);
            return decorator;
        });
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher.DomainEventDispatcher>();
    }
}

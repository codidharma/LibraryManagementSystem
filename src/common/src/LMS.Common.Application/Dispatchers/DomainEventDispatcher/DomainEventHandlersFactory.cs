using System.Reflection;
using LMS.Common.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Dispatchers.DomainEventDispatcher;

public static class DomainEventHandlersFactory
{
    public static IEnumerable<IDomainEventHandler> GetHandlers(
        Type domainEventType,
        IServiceProvider serviceProvider,
        Assembly assembly)
    {
        Type[] domainEventHandlerTypes = assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler<>).MakeGenericType(domainEventType)))
            .ToArray();

        List<IDomainEventHandler> handlers = [];
        foreach (Type domainEventHandlerType in domainEventHandlerTypes)
        {
            object domainEventHandler = serviceProvider.GetRequiredService(domainEventHandlerType);
            handlers.Add((domainEventHandler as IDomainEventHandler)!);
        }
        return handlers;
    }
}

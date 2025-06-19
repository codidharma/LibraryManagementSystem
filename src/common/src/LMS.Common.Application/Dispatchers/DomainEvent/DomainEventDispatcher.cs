using System.Reflection;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;

namespace LMS.Common.Application.Dispatchers.DomainEvent;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    }
    public async Task DispatchAsync(
        IDomainEvent domainEvent,
        Assembly assembly,
        CancellationToken cancellationToken = default)
    {
        Type domainEventType = domainEvent.GetType();
        IEnumerable<IDomainEventHandler> handlers = DomainEventHandlersFactory
            .GetHandlers(domainEventType, _serviceProvider, assembly);

        foreach (IDomainEventHandler handler in handlers)
        {
            await handler.HandleAsync(domainEvent, cancellationToken);
        }
    }


}

using System.Reflection;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using Microsoft.Extensions.Logging;

namespace LMS.Common.Application.Dispatchers.DomainEvent;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
            _logger.LogInformation("Started execution of {EventHandler} Domain Event Handler.", handler.GetType().Name);

            await handler.HandleAsync(domainEvent, cancellationToken);

            _logger.LogInformation("Finshed execution of {EventHandler} Domain Event Handler.", handler.GetType().Name);
        }

    }


}

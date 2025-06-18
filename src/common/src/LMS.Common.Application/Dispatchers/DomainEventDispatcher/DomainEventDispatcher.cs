using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Dispatchers.DomainEventDispatcher;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    }
    public async Task DispatchAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default) where TDomainEvent : IDomainEvent
    {
        IDomainEventHandler<TDomainEvent> handler = _serviceProvider.GetRequiredService<IDomainEventHandler<TDomainEvent>>();

        await handler.HandleAsync(domainEvent, cancellationToken);
    }
}

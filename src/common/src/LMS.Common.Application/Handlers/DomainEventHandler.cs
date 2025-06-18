using LMS.Common.Domain;

namespace LMS.Common.Application.Handlers;

public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public abstract Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

    public Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return HandleAsync((TDomainEvent)domainEvent, cancellationToken);

    }
}

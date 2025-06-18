using LMS.Common.Domain;

namespace LMS.Common.Application.Dispatchers.DomainEventDispatcher;

public interface IDomainEventDispatcher
{
    Task DispatchAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
        where TDomainEvent : IDomainEvent;
}

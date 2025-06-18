using LMS.Common.Application.Handlers;
using LMS.Modules.Membership.Domain.PatronAggregate.DomainEvents;
using Microsoft.Extensions.Logging;

namespace LMS.Modules.Membership.Application.Patrons.Events.PatronOnboarded;

public sealed class PatronOnboardedDomainEventHandler : DomainEventHandler<PatronOnboardedDomainEvent>
{
    private readonly ILogger _logger;

    public PatronOnboardedDomainEventHandler(ILogger<PatronOnboardedDomainEventHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public override Task HandleAsync(
        PatronOnboardedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("The event handler {Handler} was executed", nameof(PatronOnboardedDomainEventHandler));

        return Task.CompletedTask;
    }
}

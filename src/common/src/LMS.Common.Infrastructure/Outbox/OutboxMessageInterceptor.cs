using System.Text.Json;
using LMS.Common.Domain;
using LMS.Common.Infrastructrure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LMS.Common.Infrastructure.Outbox;

public sealed class OutboxMessageInterceptor : SaveChangesInterceptor
{
    //Check the entrity change tracker at the start of the save changes and insert the domain events into the outbox
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            InsertOutboxMessages(eventData.Context);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsertOutboxMessages(DbContext context)
    {
        IEnumerable<Entity> entities = context
            .ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity);

        List<IDomainEvent> domainEvents = [];

        foreach (Entity entity in entities)
        {
            domainEvents.AddRange(entity.DomainEvents);
            entity.ClearDomainEvents();
        }

        List<OutboxMessage> outboxMessages = [];

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            OutboxMessage outboxMessage = new()
            {
                Id = domainEvent.Id,
                EventType = domainEvent.GetType().Name,
                EventPayload = JsonSerializer.Serialize(domainEvent),
                IsProcessed = false,
                OccuredOnUtc = domainEvent.OccuredOnUtc,
            };
            outboxMessages.Add(outboxMessage);
        }

        context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}

using LMS.Common.Application.Dispatchers.DomainEvent;
using LMS.Common.Domain;
using LMS.Common.Infrastructrure.Outbox;
using LMS.Common.Infrastructure.JsonSerialization;
using LMS.Modules.Membership.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LMS.Modules.Membership.Infrastructure.Outbox;

internal sealed class OutboxProcessorService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly OutboxOptions _options;
    public OutboxProcessorService(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<OutboxOptions> options)
    {
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            MembershipDbContext dbContext = scope.ServiceProvider.GetRequiredService<MembershipDbContext>();
            IDomainEventDispatcher dispatcher = scope.ServiceProvider.GetRequiredService<IDomainEventDispatcher>();
            ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger<OutboxProcessorService>>();

            List<OutboxMessage> pendingMessages = await dbContext
                .OutboxMessages
                .Where(m => !m.IsProcessed)
                .OrderBy(m => m.OccuredOnUtc)
                .Take(_options.BatchSize)
                .ToListAsync(stoppingToken);

            foreach (OutboxMessage message in pendingMessages)
            {
                try
                {
                    if (message.IsProcessed)
                    {
                        continue;
                    }


                    IDomainEvent domainEvent = JsonConvert
                        .DeserializeObject<IDomainEvent>(message.EventPayload, SerializerSettings.Instance);

                    if (domainEvent is null)
                    {
                        continue;
                    }

                    await dispatcher.DispatchAsync(domainEvent!, Application.AssemblyReference.Assembly, stoppingToken);

                    message.IsProcessed = true;
                    message.ProcessedOnUtc = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to process outbox message {Id}", message.Id);
                    message.Error = ex.Message;
                }
                await dbContext.SaveChangesAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(_options.IntervalBetweenRunsInSeconds), stoppingToken);
            }

        }

    }
}


using System.Diagnostics;
using LMS.Modules.Membership.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OpenTelemetry.Trace;

namespace LMS.MigrationServices;

internal sealed class MembershipMigrationWorker : BackgroundService
{
    public const string ActivitySourceName = "MembershipModuleMigrations";
    private static readonly ActivitySource _activitySource = new(ActivitySourceName);
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    public MembershipMigrationWorker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
    {
        _serviceProvider = serviceProvider;
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using Activity? activity = _activitySource.StartActivity("Migrating membership database.", ActivityKind.Client);
        using IServiceScope scope = _serviceProvider.CreateScope();
        ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger<MembershipMigrationWorker>>();
#pragma warning disable S2139 // Exceptions should be either logged or rethrown but not both
        try
        {


            MembershipDbContext dbContext = scope.ServiceProvider.GetRequiredService<MembershipDbContext>();

            logger.LogInformation("Running the migrations.");
            await RunMigrationAsync(dbContext, stoppingToken);
            logger.LogInformation("Finished running the migrations.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed while applying migrations.");
            activity?.RecordException(ex);
            throw;
        }
#pragma warning restore S2139 // Exceptions should be either logged or rethrown but not both
        _hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(MembershipDbContext dbContext, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        });


    }
}

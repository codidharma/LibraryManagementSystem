
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
        try
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            MembershipDbContext dbContext = scope.ServiceProvider.GetRequiredService<MembershipDbContext>();
            await RunMigrationAsync(dbContext, stoppingToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }
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

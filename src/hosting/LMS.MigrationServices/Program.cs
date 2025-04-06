using LMS.MigrationServices;
using LMS.Modules.Membership.Infrastructure.Data;
using LMS.ServiceDefaults;
using Microsoft.EntityFrameworkCore.Migrations;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

IConfiguration configuration = builder.Configuration;

builder.Services.AddNpgsql<MembershipDbContext>(configuration.GetConnectionString("lmsdb"), options =>
{
    options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, LMS.Modules.Membership.Infrastructure.Data.Schema.Name);
});
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(MembershipMigrationWorker.ActivitySourceName));
builder.Services.AddHostedService<MembershipMigrationWorker>();

IHost host = builder.Build();
await host.RunAsync();

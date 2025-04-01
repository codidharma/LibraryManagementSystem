using LMS.MigrationServices;
using LMS.Modules.Membership.Infrastructure.Data;
using LMS.ServiceDefaults;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<MembershipDbContext>("lmsdb");
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(MembershipMigrationWorker.ActivitySourceName));
builder.Services.AddHostedService<MembershipMigrationWorker>();

IHost host = builder.Build();
await host.RunAsync();

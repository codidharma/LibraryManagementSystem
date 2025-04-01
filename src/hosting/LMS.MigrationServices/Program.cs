using LMS.MigrationServices;
using LMS.ServiceDefaults;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<MembershipMigrationWorker>();

IHost host = builder.Build();
await host.RunAsync();

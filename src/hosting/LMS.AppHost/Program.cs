IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .AddDatabase("lmsdb");

IResourceBuilder<SeqResource> seq = builder.AddSeq("seq")
    .ExcludeFromManifest()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithEnvironment("ACCEPT_EULA", "Y");

builder.AddProject<Projects.LMS_Api>("lmsapi")
    .WithReference(postgres)
    .WaitFor(postgres)
    .WithReference(seq)
    .WaitFor(seq);

builder.AddProject<Projects.LMS_MigrationServices>("lmsmigrationservices")
    .WithReference(postgres)
    .WaitFor(postgres);

await builder.Build().RunAsync();

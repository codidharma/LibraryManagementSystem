IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .AddDatabase("lmsdb");

builder.AddProject<Projects.LMS_Api>("lmsapi")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.AddProject<Projects.LMS_MigrationServices>("lmsmigrationservices")
    .WithReference(postgres)
    .WaitFor(postgres);

await builder.Build().RunAsync();

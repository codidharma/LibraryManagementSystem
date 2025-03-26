IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> postgres = builder.AddPostgres("postgres")
    .AddDatabase("lms_db");

builder.AddProject<Projects.LMS_API>("lms-api").WithReference(postgres);

await builder.Build().RunAsync();

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LMS_API>("lms-api");

await builder.Build().RunAsync();

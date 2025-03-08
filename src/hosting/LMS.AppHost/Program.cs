IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LMS_Modules_Membership_API>("lms-modules-membership-api");

await builder.Build().RunAsync();

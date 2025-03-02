IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LMS_Modules_IAM_Presentation>("lms-modules-iam-presentation");

await builder.Build().RunAsync();

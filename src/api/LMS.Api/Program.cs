using LMS.Api.Middleware;
using LMS.Common.Api;
using LMS.Common.Application.Dispatchers;
using LMS.Modules.Membership.Registrations;
using LMS.ServiceDefaults;
using Scalar.AspNetCore;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.AddSeqEndpoint(connectionName: "seq");
builder.AddServiceDefaults();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

builder.Services.AddDispatchers();

IConfiguration configuration = builder.Configuration;
builder.Services.RegisterMembershipModule(configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Servers = Array.Empty<ScalarServer>();
    });
}
app.MapEndpoints();
app.UseExceptionHandler();
app.UseTrackingIdVerifier();
app.UseLoggerEnricher();
app.UseTraceIdStamper();
app.UseHttpsRedirection();

await app.RunAsync();

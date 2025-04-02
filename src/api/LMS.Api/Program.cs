using LMS.Common.Api;
using LMS.Common.Application.Dispatchers;
using LMS.Modules.Membership.Registrations;
using LMS.ServiceDefaults;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDispatchers();
IConfiguration configuration = builder.Configuration;
builder.Services.RegisterMembershipModule(configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapEndpoints();

app.UseHttpsRedirection();

await app.RunAsync();

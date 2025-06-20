using FluentValidation;
using LMS.Common.Api;
using LMS.Common.Application.Handlers;
using LMS.Common.Infrastructure.Outbox;
using LMS.Modules.Membership.Application.Common.Identity;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data;
using LMS.Modules.Membership.Infrastructure.Data.Repositories;
using LMS.Modules.Membership.Infrastructure.Identity;
using LMS.Modules.Membership.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Modules.Membership.Registrations;

public static class RegistrationsExtensions
{
    public static IServiceCollection RegisterMembershipModule(this IServiceCollection services, IConfiguration configuration)
    {
        AddApi(services);
        AddApplication(services);
        AddInfrastructure(services, configuration);
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MembershipDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("lmsdb")
                ?? throw new InvalidOperationException("Connection string 'lmsdb' not found."),
                npgSqlOptions => npgSqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema.Name));
            options.AddInterceptors(sp.GetRequiredService<OutboxMessageInterceptor>());
        });

        services.AddOptions<OutboxOptions>()
            .Bind(configuration.GetSection("Memberships:Outbox"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IPatronRepository, PatronRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddHostedService<OutboxProcessorService>();
    }

    private static void AddApplication(this IServiceCollection services)
    {

        //Automatically register all the validators
        services.AddValidatorsFromAssembly(AssemblyReferences.ApplicationAssembly);
        services.AddHandlersFromAssemblies(AssemblyReferences.ApplicationAssembly);
    }

    private static void AddApi(this IServiceCollection services)
    {
        services.AddEndpoints(AssemblyReferences.ApiAssembly);
    }
}

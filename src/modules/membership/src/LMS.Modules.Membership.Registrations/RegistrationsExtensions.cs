using FluentValidation;
using LMS.Common.Api;
using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Modules.Membership.Application.Common.Identity;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data;
using LMS.Modules.Membership.Infrastructure.Data.Repositories;
using LMS.Modules.Membership.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Modules.Membership.Registrations;

public static class RegistrationsExtensions
{
    public static IServiceCollection RegisterMembershipModule(this IServiceCollection services)
    {
        AddApi(services);
        AddApplication(services);
        AddInfrastructure(services);
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<MembershipDbContext>(options =>
        {
            options.UseNpgsql("lmsdb");
        });
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MembershipDbContext>());
        services.AddScoped<IPatronRepository, PatronRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
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

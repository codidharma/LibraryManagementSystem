using LMS.Modules.Membership.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Modules.Membership.Registrations;

public static class RegistrationsExtension
{
    public static IServiceCollection RegisterMembershipModule(this IServiceCollection services)
    {
        AddInfrastructure(services);
        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<MembershipDbContext>(options =>
        {
            options.UseNpgsql("lms_db");
        });
    }
}

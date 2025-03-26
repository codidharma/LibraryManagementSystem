using LMS.Modules.Membership.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Modules.Membership.Infrastructure;

public static class MembershipModule
{
    public static IServiceCollection AddMembershipModule(this IServiceCollection services)
    {
        services.AddDbContext<MembershipDbContext>(options =>
        {
            options.UseNpgsql("lms_db");
        });
        return services;
    }
}

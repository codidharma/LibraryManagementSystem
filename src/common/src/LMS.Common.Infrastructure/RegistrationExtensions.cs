using LMS.Common.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LMS.Common.Infrastructure;

public static class RegistrationExtensions
{
    public static void AddCommonInfrastructure(this IServiceCollection services)
    {
        services.TryAddSingleton<OutboxMessageInterceptor>();
    }
}

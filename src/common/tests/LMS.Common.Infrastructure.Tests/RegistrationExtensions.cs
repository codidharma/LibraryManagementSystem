using LMS.Common.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LMS.Common.Infrastructure.Tests;

public static class RegistrationExtensions
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.TryAddSingleton<OutboxMessageInterceptor>();
        return services;
    }
}

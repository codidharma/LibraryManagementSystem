using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Handlers;

public static class HandlerExtensions
{
    public static IServiceCollection AddHandlersFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {
        AddCommandHandlersFromAssemblies(services, assemblies);
        return services;
    }
    private static void AddCommandHandlersFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {
        Type[] types = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => type is { IsAbstract: false, IsInterface: false }
                && type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
            .ToArray();

        foreach (Type type in types)
        {
            IEnumerable<Type> interfaceTypes = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
            foreach (Type interfaceType in interfaceTypes)
            {
                services.AddScoped(interfaceType, type);
            }
        }
    }
}

namespace Registrator.Net;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Automatically registers types in the specified assemblies with the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AutoRegisterTypesInAssemblies(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        if (assemblies.Length == 0)
        {
            return services;
        }

        foreach (Assembly assembly in assemblies)
        {
            List<Type> types = assembly
                .GetTypes()
                .Where(t => t.GetCustomAttributes<AutoRegisterType>().Any()).ToList();
            foreach (Type type in types)
            {
                AutoRegisterType attribute = type.GetCustomAttribute<AutoRegisterType>()!;
                services.Add(new ServiceDescriptor(type, type, attribute.Lifetime));
            }

            types = assembly
                .GetTypes()
                .Where(t => t.GetCustomAttributes<AutoRegisterInterfaces>().Any()).ToList();
            foreach (Type type in types)
            {
                AutoRegisterInterfaces attribute = type.GetCustomAttribute<AutoRegisterInterfaces>()!;
                List<Type> interfacesToRegister = type.GetInterfaces()
                    .Where(@interface =>
                        @interface != typeof(IDisposable)
                        && @interface != typeof(IAsyncDisposable)
                        && @interface != typeof(ISerializable)
                    ).ToList();

                if (attribute.Lifetime == ServiceLifetime.Transient)
                {
                    //All the resolved instances must be different
                    foreach (Type @interface in interfacesToRegister)
                    {
                        services.Add(new ServiceDescriptor(@interface, type, attribute.Lifetime));
                    }
                }
                else
                {
                    //All the resolved instances must be resolved to the same instance
                    Type type0 = interfacesToRegister.FirstOrDefault();
                    if (type0 is null)
                    {
                        continue;
                    }

                    services.Add(new ServiceDescriptor(type0, type, attribute.Lifetime));

                    foreach (Type @interface in interfacesToRegister.Skip(1))
                    {
                        services.Add(new ServiceDescriptor(@interface, sp => sp.GetRequiredService(type0), attribute.Lifetime));
                    }
                }
            }

            types = assembly
                .GetTypes()
                .Where(t => t.GetCustomAttributes<AutoRegisterTypeAndInterfaces>().Any()).ToList();

            foreach (Type type in types)
            {
                AutoRegisterTypeAndInterfaces attribute =
                    type.GetCustomAttribute<AutoRegisterTypeAndInterfaces>()!;
                services.Add(new ServiceDescriptor(type, type, attribute.Lifetime));
                IEnumerable<Type> interfacesToRegister = type.GetInterfaces()
                    .Where(@interface =>
                        @interface != typeof(IDisposable)
                        && @interface != typeof(IAsyncDisposable)
                        && @interface != typeof(ISerializable)
                    );
                foreach (Type @interface in interfacesToRegister)
                {
                    services.Add(
                        new ServiceDescriptor(
                            @interface,
                            sp => sp.GetRequiredService(type),
                            attribute.Lifetime
                        )
                    );
                }
            }
        }

        return services;
    }
}

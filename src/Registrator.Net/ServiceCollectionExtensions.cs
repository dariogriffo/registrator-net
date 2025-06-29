namespace Registrator.Net;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Automatically registers types in the specified assemblies with the DI container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/></param>
    /// <param name="assemblies">A collection of assemblies to scan to auto register</param>
    /// <returns></returns>
    public static IServiceCollection AutoRegisterTypesInAssemblies(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        return services.AutoRegisterTypesInAssemblies(c => c.Assemblies = assemblies);
    }

    /// <summary>
    /// Automatically registers types in the specified assemblies with the DI container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/></param>
    /// <param name="configurer">The configurator Action to register your types</param>
    /// <returns></returns>
    public static IServiceCollection AutoRegisterTypesInAssemblies(
        this IServiceCollection services,
        Action<RegistratorConfiguration> configurer
    )
    {
        bool CanRegisterType(Type t, HashSet<string> namespaces, HashSet<string> tags)
        {
            string? tag = t.GetCustomAttribute<AutoRegisterType>()?.Tag ??
                          t.GetCustomAttribute<AutoRegisterInterfaces>()?.Tag ??
                          t.GetCustomAttribute<AutoRegisterTypeAndInterfaces>()?.Tag;
            
            bool canRegisterTypeWithTag = tag is null || tags.Contains(tag);

            return canRegisterTypeWithTag && !namespaces.Contains(t.Namespace ?? string.Empty);
        }

        RegistratorConfiguration configuration = new();
        configurer.Invoke(configuration);

        if (configuration.Assemblies.Length == 0)
        {
            return services;
        }

        HashSet<string> excludedNamespaces = configuration.ExcludedTypesNamespaces
            .Where(t => t.Namespace is not null)
            .Select(t => t.Namespace ?? string.Empty)
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .ToHashSet();

        HashSet<string> tags = configuration.Tags.ToHashSet();

        Assembly[] assemblies = configuration.Assemblies;

        IEnumerable<Type> excludedInterfacesFromInterfaces = configuration
            .ExcludedAssemblies.SelectMany(assembly =>
                assembly.GetTypes().Where(t => t.IsInterface)
            )
            .Distinct();

        HashSet<Type> excludedInterfaces =
        [
            typeof(IDisposable),
            typeof(IAsyncDisposable),
            typeof(ISerializable),
        ];

        excludedInterfaces.UnionWith(excludedInterfacesFromInterfaces);

        foreach (Assembly assembly in assemblies)
        {
            List<Type> types = [];
            List<Type> interfaces = [];
            List<Type> typesAndInterfaces = [];
            foreach (var t in assembly.GetTypes())
            {
                if (
                    t.GetCustomAttributes<AutoRegisterType>().Any()
                    && CanRegisterType(t, excludedNamespaces, tags)
                )
                {
                    types.Add(t);
                }

                if (
                    t.GetCustomAttributes<AutoRegisterInterfaces>().Any()
                    && CanRegisterType(t, excludedNamespaces, tags)
                )
                {
                    interfaces.Add(t);
                }

                if (
                    t.GetCustomAttributes<AutoRegisterTypeAndInterfaces>().Any()
                    && CanRegisterType(t, excludedNamespaces, tags)
                )
                {
                    typesAndInterfaces.Add(t);
                }
            }

            AutoRegisterTypes(services, types);

            AutoRegisterInterfaces(services, interfaces, excludedInterfaces);

            AutoRegisterTypeAndInterfaces(services, typesAndInterfaces, excludedInterfaces);
        }

        return services;
    }

    private static void AutoRegisterTypeAndInterfaces(
        IServiceCollection services,
        List<Type> types,
        HashSet<Type> excludedInterfaces
    )
    {
        foreach (Type type in types)
        {
            AutoRegisterTypeAndInterfaces attribute =
                type.GetCustomAttribute<AutoRegisterTypeAndInterfaces>()!;
            services.Add(
                attribute.Key is null
                    ? new ServiceDescriptor(type, type, attribute.Lifetime)
                    : new ServiceDescriptor(type, attribute.Key, type, attribute.Lifetime)
            );

            List<Type> interfacesToRegister = type.GetInterfaces()
                .Where(@interface => !excludedInterfaces.Contains(@interface))
                .Where(@interface =>
                    !(
                        @interface.IsGenericType
                        && excludedInterfaces.Contains(@interface.GetGenericTypeDefinition())
                    )
                )
                .ToList();
            foreach (Type @interface in interfacesToRegister)
            {
                if (attribute.Key is null)
                {
                    services.Add(
                        new ServiceDescriptor(
                            @interface,
                            sp => sp.GetRequiredService(type),
                            attribute.Lifetime
                        )
                    );
                }
                else
                {
                    services.Add(
                        new ServiceDescriptor(
                            @interface,
                            attribute.Key,
                            (sp, o) => sp.GetRequiredKeyedService(type, o),
                            attribute.Lifetime
                        )
                    );
                }
            }
        }
    }

    private static void AutoRegisterInterfaces(
        IServiceCollection services,
        List<Type> types,
        HashSet<Type> excludedInterfaces
    )
    {
        foreach (Type type in types)
        {
            AutoRegisterInterfaces attribute = type.GetCustomAttribute<AutoRegisterInterfaces>()!;
            List<Type> interfacesToRegister = type.GetInterfaces()
                .Where(@interface => !excludedInterfaces.Contains(@interface))
                .Where(@interface =>
                    !(
                        @interface.IsGenericType
                        && excludedInterfaces.Contains(@interface.GetGenericTypeDefinition())
                    )
                )
                .ToList();

            if (attribute.Lifetime == ServiceLifetime.Transient)
            {
                //All the resolved instances must be different
                foreach (Type @interface in interfacesToRegister)
                {
                    if (attribute.Key is null)
                    {
                        services.Add(new ServiceDescriptor(@interface, type, attribute.Lifetime));
                    }
                    else
                    {
                        services.Add(
                            new ServiceDescriptor(
                                @interface,
                                attribute.Key,
                                type,
                                attribute.Lifetime
                            )
                        );
                    }
                }
            }
            else
            {
                Type? type0 = interfacesToRegister.FirstOrDefault();
                if (type0 is null)
                {
                    continue;
                }

                if (attribute.Key is null)
                {
                    //All the resolved instances must be resolved to the same instance
                    services.Add(new ServiceDescriptor(type0, type, attribute.Lifetime));

                    IEnumerable<Type> otherInterfaces = interfacesToRegister.Skip(1);
                    foreach (Type @interface in otherInterfaces)
                    {
                        services.Add(
                            new ServiceDescriptor(
                                @interface,
                                sp => sp.GetRequiredService(type0),
                                attribute.Lifetime
                            )
                        );
                    }
                }
                else
                {
                    //All the resolved instances must be resolved to the same instance
                    services.Add(new ServiceDescriptor(type, type, attribute.Lifetime));

                    IEnumerable<Type> otherInterfaces = interfacesToRegister;
                    foreach (Type @interface in otherInterfaces)
                    {
                        services.Add(
                            new ServiceDescriptor(
                                @interface,
                                attribute.Key,
                                (sp, _) => sp.GetRequiredService(type),
                                attribute.Lifetime
                            )
                        );
                    }
                }
            }
        }
    }

    private static void AutoRegisterTypes(IServiceCollection services, List<Type> types)
    {
        foreach (Type type in types)
        {
            AutoRegisterType attribute = type.GetCustomAttribute<AutoRegisterType>()!;
            if (attribute.Lifetime is ServiceLifetime.Transient)
            {
                if (attribute.Key is null)
                {
                    services.Add(new ServiceDescriptor(type, type, attribute.Lifetime));
                }
                else
                {
                    services.Add(
                        new ServiceDescriptor(type, attribute.Key, type, attribute.Lifetime)
                    );
                }
            }
            else
            {
                services.Add(new ServiceDescriptor(type, type, attribute.Lifetime));

                if (attribute.Key is not null)
                {
                    services.Add(
                        new ServiceDescriptor(
                            type,
                            attribute.Key,
                            (sp, _) => sp.GetRequiredService(type),
                            attribute.Lifetime
                        )
                    );
                }
            }
        }
    }
}

namespace Registrator.Net;

using System;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Attribute to automatically register a type with the DI container.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AutoRegisterType : Attribute
{
    internal ServiceLifetime Lifetime { get; }
    internal object? Key { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoRegisterInterfaces"/> class.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> assigned to the class resolving the type</param>
    /// <param name="key">An optional key to register the class</param>
    public AutoRegisterType(ServiceLifetime lifetime = ServiceLifetime.Scoped, object? key = null)
    {
        Lifetime = lifetime;
        Key = key;
    }
}

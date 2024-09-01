namespace Registrator.Net;

using System;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Attribute to automatically register all the interfaces of the type with the DI container.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AutoRegisterInterfaces : Attribute
{
    internal ServiceLifetime Lifetime { get; }
    internal object? Key { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoRegisterInterfaces"/> class.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> assigned to the class resolving interfaces</param>
    /// <param name="key">An optional key to register the class</param>
    public AutoRegisterInterfaces(
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        object? key = null
    )
    {
        Lifetime = lifetime;
        Key = key;
    }
}

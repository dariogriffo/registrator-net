namespace Registrator.Net;

using Microsoft.Extensions.DependencyInjection;
using System;

/// <summary>
/// Attribute to automatically register all the interfaces of the type with the DI container.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AutoRegisterInterfaces : Attribute
{
    internal ServiceLifetime Lifetime { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoRegisterInterfaces"/> class.
    /// </summary>
    /// <param name="lifetime"></param>
    public AutoRegisterInterfaces(ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Lifetime = lifetime;
    }
}

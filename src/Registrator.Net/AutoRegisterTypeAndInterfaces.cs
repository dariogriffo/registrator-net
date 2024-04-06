namespace Registrator.Net;

using Microsoft.Extensions.DependencyInjection;
using System;

/// <summary>
/// Attribute to automatically register a type and its interfaces with the DI container.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AutoRegisterTypeAndInterfaces : Attribute
{
    internal ServiceLifetime Lifetime { get; }


    /// <summary>
    /// Initializes a new instance of the <see cref="AutoRegisterTypeAndInterfaces"/> class.
    /// </summary>
    /// <param name="lifetime"></param>
    public AutoRegisterTypeAndInterfaces(ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Lifetime = lifetime;
    }
}

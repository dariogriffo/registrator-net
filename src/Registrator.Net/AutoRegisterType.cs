namespace Registrator.Net;

using Microsoft.Extensions.DependencyInjection;
using System;

/// <summary>
/// Attribute to automatically register a type with the DI container.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AutoRegisterType : Attribute
{
    internal ServiceLifetime Lifetime { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoRegisterType"/> class.
    /// </summary>
    /// <param name="lifetime"></param>
    public AutoRegisterType(ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Lifetime = lifetime;
    }
}

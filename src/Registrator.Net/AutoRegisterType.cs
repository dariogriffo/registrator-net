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
    internal string? Tag { get; }
    internal int? Order { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoRegisterInterfaces"/> class.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> assigned to the type resolving the type</param>
    /// <param name="key">An optional key to register the type</param>
    /// <param name="tag">An optional tag to register the type. The type will be registered only if the tag is null or present in the <see cref="RegistratorConfiguration.Tags"/> </param>
    /// <param name="order">An optional order to register the type. The types will be registered in ascending order</param>
    public AutoRegisterType(
        ServiceLifetime lifetime = ServiceLifetime.Scoped, 
        object? key = null, 
        string? tag = null,
        int order = -1
    )
    {
        Lifetime = lifetime;
        Key = key;
        Tag = tag;
        Order = order == -1 ? null : order;
    }
}

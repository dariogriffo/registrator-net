namespace Registrator.Net;

using System;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Attribute to automatically register a type and its interfaces with the DI container.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AutoRegisterTypeAndInterfaces : Attribute
{
    internal ServiceLifetime Lifetime { get; }
    internal object? Key { get; }
    internal string? Tag { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoRegisterTypeAndInterfaces"/> class.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> assigned to the type resolving the type and interfaces</param>
    /// <param name="key">An optional key to register the type</param>
    /// <param name="tag">An optional tag to register the type. The type and interfaces will be registered only if the tag is null or present in the <see cref="RegistratorConfiguration.Tags"/> </param>
    public AutoRegisterTypeAndInterfaces(
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        object? key = null,
        string? tag = null
    )
    {
        Lifetime = lifetime;
        Key = key;
        Tag = tag;
    }
}

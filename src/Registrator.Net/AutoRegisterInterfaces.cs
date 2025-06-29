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
    
    internal string? Tag { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoRegisterInterfaces"/> class.
    /// </summary>
    /// <param name="lifetime">The <see cref="ServiceLifetime"/> assigned to the type resolving interfaces</param>
    /// <param name="key">An optional key to register the type</param>
    /// <param name="tag">An optional set of tags to enable the register the type. The interfaces will be registered only if the tag is null or present in the <see cref="RegistratorConfiguration.Tags"/> </param>
    public AutoRegisterInterfaces(
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

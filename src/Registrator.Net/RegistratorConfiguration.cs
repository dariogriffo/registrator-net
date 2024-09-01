namespace Registrator.Net;

using System;
using System.Reflection;

/// <summary>
/// The configuration for the registrator.
/// </summary>
public class RegistratorConfiguration
{
    /// <summary>
    /// Assemblies to scan for types to register.
    /// </summary>
    public Assembly[] Assemblies { get; set; } = Array.Empty<Assembly>();

    /// <summary>
    /// Assemblies to exclude interfaces from register on types declared in <see cref="Assemblies"/>.
    /// </summary>
    public Assembly[] ExcludedAssemblies { get; set; } = Array.Empty<Assembly>();
}

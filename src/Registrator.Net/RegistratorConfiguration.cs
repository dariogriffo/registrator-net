namespace Registrator.Net;

using System;
using System.Reflection;

/// <summary>
/// The configuration for the registrator.
/// </summary>
public sealed class RegistratorConfiguration
{
    /// <summary>
    /// Assemblies to scan for types to register.
    /// </summary>
    public Assembly[] Assemblies { get; set; } = [];

    /// <summary>
    /// Assemblies to exclude interfaces from register on types declared in <see cref="Assemblies"/>.
    /// </summary>
    public Assembly[] ExcludedAssemblies { get; set; } = [];
    
    /// <summary>
    /// Namespaces to exclude from register declared in <see cref="Assemblies"/>.
    /// Example, given a class with fully qualified name "MyCompany.Services.MyService"
    /// You can exclude all types in the "MyCompany.Services" namespace by doing
    /// ExcludedNamespaces = [typeof(MyService)]
    /// </summary>
    public Type[] ExcludedTypesNamespaces { get; set; } = [];
    
    /// <summary>
    /// Tags to filter the registration of types.
    /// When provided only types with the no tag or matching will be registered.
    /// </summary>
    public string[] Tags { get; set; } = [];
}

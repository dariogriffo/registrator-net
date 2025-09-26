[![N|Solid](https://avatars2.githubusercontent.com/u/39886363?s=200&v=4)](https://github.com/dariogriffo/registrator-net)

# Registrator.Net

A simple yet effective auto registration tool for dotnet

[![NuGet Info](https://img.shields.io/nuget/dt/Registrator.Net)](https://www.nuget.org/packages/Registrator.Net/)
![GitHub License](https://img.shields.io/github/license/dariogriffo/registrator-net)
![CI](https://github.com/dariogriffo/registrator-net/workflows/CI/badge.svg)


## Table of contents
- [Notes](#notes)
- [About](#about)
- [Getting Started](#getting-started)
- [Advanced Usage](#advanced-usage)
- [License](#license)

## Notes

Doesn't support automatic registration of generic types, you will have to register them manually.

## About

[Registrator.Net](https://www.nuget.org/packages/Registrator.Net) is a simple auto registration tool for dotnet.

I have been trying to find a simple tool to register my internal dependencies and didn't find any that I liked, so I decided to create my own.

### Who is it for?

[Registrator.Net](https://www.nuget.org/packages/Registrator.Net) is intended for developers who want something simple that just works.

It is not designed to deal with every case, but the simple ones, the ones you will be doing 99% of the time.

## Getting Started

### Installation
`Install-Package Registrator.Net`
                                                      
Tag classes, records and structs with any of the following attributes:
- AutoRegisterType
- AutoRegisterTypeAndInterfaces
- AutoRegisterInterfaces

Then in your Program.cs or Startup.cs, call `services.AutoRegisterTypesInAssemblies(assembly1,assembly2,assembly3...);`

If you want to skip the registration of types that implement a certain interface from a certain assembly, 
you can call 
```csharp
services.AutoRegisterTypesInAssemblies(new RegistratorConfiguration()
{
    Assemblies = [typeof(ConcreteType).Assembly],
    ExcludedAssemblies = [typeof(IRequestHandler<>).Assembly,typeof(IMediator).Assembly]
});
```

By default all registered types are registered as `ServiceLifetime.Scoped`, but you can change it by passing a `ServiceLifetime` as a parameter of the attribute.
You can also add keyed services if you use the `Key` property of the attribute.
~~~~csharp
[AutoRegisterInterfaces(ServiceLifetime.Scoped, "17")]
public class ConcreteType17 : IInterface25, IInterface26 { }
~~~~
In the above the interfaces of the class with be resolved to ConcreteType17 when requested with the key `17`


## Advanced Usage

### Tagging

You can tag your types with the `tag` property of the attributes. If a tag is provided, the type will be registered only if the tag is null or present in the `RegistratorConfiguration.Tags` array or if the tag is null.

Example
~~~~csharp
[AutoRegisterTypeAndInterfaces(ServiceLifetime.Transient, tag: "tag1")]
public class ConcreteTypeWithMatchingTags : IInterfaceAndTypeWithMatchingTags { }

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Transient, tag: "tag2")]
public class ConcreteTypeWithNoMatchingTags : IInterfaceAndTypeWithNoMatchingTags { }

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Transient)]
public class ConcreteTypeWithNoTags : IInterfaceAndTypeWithNoTags { }

...
    
    services.AutoRegisterTypesInAssemblies(c =>
    {
        c.Assemblies = [typeof(ConcreteType).Assembly];
        c.Tags = ["tag1"];
    });
~~~~

In the above example, only `ConcreteTypeWithMatchingTags` and `ConcreteTypeWithNoTags` will be registered.

### Excluding types from registration by namespace

You can exclude types from registration by namespace by using the `ExcludedTypesNamespaces` property of the `RegistratorConfiguration` class.
The decision to use Types to discover the namespace was made to avoid the use of strings and make it more type safe.
Example:
~~~~csharp
services.AutoRegisterTypesInAssemblies(c =>
{
    c.Assemblies = [typeof(ConcreteType).Assembly];
    c.ExcludedTypesNamespaces = [typeof(ClassToExclude)];
});
~~~~
In the above example, all types sharing the namespace of `ClassToExclude` will be excluded from registration.

## License

[MIT](https://github.com/dariogriffo/registrator-net/blob/main/LICENSE)

### Logo
Logo Provided by [Vecteezy](https://vecteezy.com)

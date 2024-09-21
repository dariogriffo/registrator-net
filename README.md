[![N|Solid](https://avatars2.githubusercontent.com/u/39886363?s=200&v=4)](https://github.com/dariogriffo/registator-net)

# Registrator.Net

A simple yet effective auto registration tool for dotnet

[![NuGet Info](https://img.shields.io/nuget/dt/Registrator.Net)](https://www.nuget.org/packages/Registrator.Net/)
![GitHub License](https://img.shields.io/github/license/dariogriffo/registrator-net)
![CI](https://github.com/dariogriffo/registrator-net/workflows/.NET/badge.svg)


## Table of contents
- [Notes](#notes)
- [About](#about)
- [Getting Started](#getting-started)
- [License](#license)

## Notes

Doesn't support automatic registration of generic types, you will have to register them manually.

## About

[Registrator.Net](https://www.nuget.org/packages/Registrator.Net) is a simple AutoRegistration tool for dotnet.

I have been trying to find a simple tool to register my internal dependencies and didn't find any that I liked, so I decided to create my own.

### Who is it for?

[Registrator.Net](https://www.nuget.org/packages/Registrator.Net) is intended for developers who want something simple that just works.

It is not designed to deal with every case, but the simple ones, the ones you will be doing 99% of the time.

## Getting Started

### With Contracts implementation
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
~~~~
~~~~
## License

[MIT](https://github.com/dariogriffo/registator-net/blob/main/LICENSE)

### Logo
Logo Provided by [Vecteezy](https://vecteezy.com)

[![N|Solid](https://avatars2.githubusercontent.com/u/39886363?s=200&v=4)](https://github.com/dariogriffo/registator-net)

# Registrator.Net

A simple yet effective AutoRegistration tool for dotnet

[![NuGet Info](https://buildstats.info/nuget/Registrator.Net?includePreReleases=true)](https://www.nuget.org/packages/Registrator.Net/)
[![GitHub license](https://img.shields.io/github/license/dariogriffo/registator-net.svg)](https://raw.githubusercontent.com/dariogriffo/registator-net/master/LICENSE)
### Build Status
![.Net8.0](https://github.com/dariogriffo/registator-net/workflows/.NET/badge.svg?branch=main)

[![Build history](https://buildstats.info/github/chart/dariogriffo/registator-net?branch=main&includeBuildsFromPullRequest=false)](https://github.com/dariogriffo/registator-net/actions?query=branch%3Amain++)


## Table of contents

- [About](#about)
- [Getting Started](#getting-started)
- [License](#license)

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

By default all registered types are registered as `ServiceLifetime.Scoped`, but you can change it by passing a `ServiceLifetime` as a parameter of the attribute.

## License

[MIT](https://github.com/dariogriffo/registator-net/blob/main/LICENSE)

### Logo
Logo Provided by [Vecteezy](https://vecteezy.com)

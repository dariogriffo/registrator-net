namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterInterfaces(ServiceLifetime.Singleton, "13")]
public class ConcreteType13 : IInterface21, IInterface22 { }

[AutoRegisterInterfaces(ServiceLifetime.Scoped, "14")]
public class ConcreteType14 : IInterface21, IInterface22 { }

public interface IInterface21 { }

public interface IInterface22 { }

namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterInterfaces(ServiceLifetime.Scoped, "11")]
public class ConcreteType11 : IInterface19, IInterface20 { }

[AutoRegisterInterfaces(ServiceLifetime.Scoped, "12")]
public class ConcreteType12 : IInterface19, IInterface20 { }

public interface IInterface19 { }

public interface IInterface20 { }

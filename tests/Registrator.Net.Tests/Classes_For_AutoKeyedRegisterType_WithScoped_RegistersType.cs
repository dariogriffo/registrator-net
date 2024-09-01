namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterType(ServiceLifetime.Scoped, "17")]
public class ConcreteType17 : IInterface25, IInterface26 { }

public interface IInterface25 { }

public interface IInterface26 { }

[AutoRegisterType(ServiceLifetime.Scoped, "18")]
public class ConcreteType18 { }

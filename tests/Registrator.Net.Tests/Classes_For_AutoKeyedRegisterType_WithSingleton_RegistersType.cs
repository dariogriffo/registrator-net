namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterType(ServiceLifetime.Singleton, "19")]
public class ConcreteType19 : IInterface27, IInterface28 { }

public interface IInterface27 { }

public interface IInterface28 { }

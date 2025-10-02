namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterType(ServiceLifetime.Scoped)]
internal class ConcreteType5 : IInterface9, IInterface10 { }

public interface IInterface9 { }

public interface IInterface10 { }

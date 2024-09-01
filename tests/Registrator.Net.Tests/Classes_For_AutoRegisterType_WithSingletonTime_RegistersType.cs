namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterType(ServiceLifetime.Singleton)]
public class ConcreteType4 : IInterface7, IInterface8 { }

public interface IInterface7 { }

public interface IInterface8 { }

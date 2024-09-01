namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Scoped)]
public class ConcreteType : IInterface1, IInterface2 { }

public interface IInterface2 { }

public interface IInterface1 { }

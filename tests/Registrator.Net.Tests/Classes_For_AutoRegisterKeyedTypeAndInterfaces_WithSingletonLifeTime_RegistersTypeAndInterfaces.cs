namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Singleton, key: "23")]
public class ConcreteType23 : IInterface35, IInterface36 { }

public interface IInterface35 { }

public interface IInterface36 { }

namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Transient, key: "22")]
public class ConcreteType22 : IInterface33, IInterface34 { }

public interface IInterface33 { }

public interface IInterface34 { }

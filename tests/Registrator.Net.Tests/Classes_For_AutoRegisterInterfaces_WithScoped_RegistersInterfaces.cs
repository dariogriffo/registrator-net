namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterInterfaces(ServiceLifetime.Scoped)]
public class ConcreteType9 : IInterface17, IInterface18 { }

public interface IInterface17 { }

public interface IInterface18 { }

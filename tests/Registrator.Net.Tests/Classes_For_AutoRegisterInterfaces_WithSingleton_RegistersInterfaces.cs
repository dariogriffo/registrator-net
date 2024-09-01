namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterInterfaces(ServiceLifetime.Singleton)]
public class ConcreteType8 : IInterface15, IInterface16 { }

public interface IInterface15 { }

public interface IInterface16 { }

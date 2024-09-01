namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterType(ServiceLifetime.Transient, "20")]
public class ConcreteType20 : IInterface29, IInterface30 { }

public interface IInterface29 { }

public interface IInterface30 { }

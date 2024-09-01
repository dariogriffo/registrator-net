namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterType(ServiceLifetime.Transient)]
public class ConcreteType6 : IInterface11, IInterface12 { }

public interface IInterface11 { }

public interface IInterface12 { }

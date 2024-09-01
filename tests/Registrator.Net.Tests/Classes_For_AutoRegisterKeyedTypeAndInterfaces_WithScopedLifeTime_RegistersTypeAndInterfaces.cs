namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Scoped, key: "21")]
public class ConcreteType21 : IInterface31, IInterface32 { }

public interface IInterface32 { }

public interface IInterface31 { }

namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Singleton)]
public class ConcreteType3 : IInterface5, IInterface6
{
}

public interface IInterface5
{
}

public interface IInterface6
{
}

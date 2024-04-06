namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Transient)]
public class ConcreteType2 : IInterface3, IInterface4
{
}

public interface IInterface3
{
}

public interface IInterface4
{
}

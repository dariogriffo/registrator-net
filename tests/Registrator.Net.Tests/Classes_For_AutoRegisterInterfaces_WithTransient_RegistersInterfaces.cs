namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterInterfaces(ServiceLifetime.Transient)]
public class ConcreteType7 : IInterface13, IInterface14
{
}

public interface IInterface13
{
}

public interface IInterface14
{
}

namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

[AutoRegisterInterfaces(ServiceLifetime.Transient, "15")]
public class ConcreteType15 : IInterface23, IInterface24 { }

[AutoRegisterInterfaces(ServiceLifetime.Transient, "16")]
public class ConcreteType16 : IInterface23, IInterface24 { }

public interface IInterface23 { }

public interface IInterface24 { }

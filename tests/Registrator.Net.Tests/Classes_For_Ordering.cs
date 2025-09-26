namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;


[AutoRegisterType(ServiceLifetime.Transient)]
public class ConcreteTypeWithNoOrder { }


[AutoRegisterType(ServiceLifetime.Transient, order: 1)]
public class ConcreteTypeWithOrder1 { }

[AutoRegisterType(ServiceLifetime.Transient, order: 2)]
public class ConcreteTypeWithOrder2 { }

[AutoRegisterType(ServiceLifetime.Transient, order: 3)]
public class ConcreteTypeWithOrder3 { }

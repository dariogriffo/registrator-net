namespace Registrator.Net.Tests;

using Microsoft.Extensions.DependencyInjection;

public interface IInterfaceAndTypeWithMatchingTags { }

public interface IInterfaceAndTypeWithNoTags { }

public interface IInterfaceAndTypeWithNoMatchingTags { }

public interface IInterfaceWithMatchingTags { }

public interface IInterfaceWithNoMatchingTags { }

public interface IInterfaceWithNoTags { }

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Transient, tag: "tag1")]
public class ConcreteTypeWithMatchingTags : IInterfaceAndTypeWithMatchingTags { }

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Transient, tag: "tag2")]
public class ConcreteTypeWithNoMatchingTags : IInterfaceAndTypeWithNoMatchingTags { }

[AutoRegisterTypeAndInterfaces(ServiceLifetime.Transient)]
public class ConcreteTypeWithNoTags : IInterfaceAndTypeWithNoTags { }

[AutoRegisterType(ServiceLifetime.Transient, tag: "tag1")]
public class ConcreteTypeWithMatchingTags2 { }

[AutoRegisterType(ServiceLifetime.Transient, tag: "tag2")]
public class ConcreteTypeWithNoMatchingTags2 { }

[AutoRegisterType(ServiceLifetime.Transient)]
public class ConcreteTypeWithNoTags2 { }

[AutoRegisterInterfaces(ServiceLifetime.Transient, tag: "tag1")]
public class ConcreteTypeWithMatchingTags3 : IInterfaceWithMatchingTags { }

[AutoRegisterInterfaces(ServiceLifetime.Transient, tag: "tag2")]
public class ConcreteTypeWithNoMatchingTags3 : IInterfaceWithNoMatchingTags { }

[AutoRegisterInterfaces(ServiceLifetime.Transient)]
public class ConcreteTypeWithNoTags3 : IInterfaceWithNoTags { }

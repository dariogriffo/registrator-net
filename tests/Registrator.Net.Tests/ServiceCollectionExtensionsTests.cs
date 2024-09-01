// ReSharper disable InconsistentNaming
namespace Registrator.Net.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AutoRegisterTypeAndInterfaces_WithScopedLifeTime_RegistersTypeAndInterfaces()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface1 instance1 = provider.GetRequiredService<IInterface1>();
        IInterface2 instance2 = provider.GetRequiredService<IInterface2>();
        ConcreteType instance3 = provider.GetRequiredService<ConcreteType>();

        instance1.Should().Be(instance3);
        instance1.Should().Be(instance2);
        instance2.Should().Be(instance3);
    }

    [Fact]
    public void AutoRegisterTypeAndInterfaces_WithTransientLifeTime_RegistersTypeAndInterfaces()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface3 instance1 = provider.GetRequiredService<IInterface3>();
        IInterface4 instance2 = provider.GetRequiredService<IInterface4>();
        ConcreteType2 instance3 = provider.GetRequiredService<ConcreteType2>();

        instance1.Should().NotBe(instance3);
        instance1.Should().NotBe(instance2);
        instance2.Should().NotBe(instance3);
    }

    [Fact]
    public void AutoRegisterTypeAndInterfaces_WithSingletonLifeTime_RegistersTypeAndInterfaces()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType3).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface5 instance5 = provider.GetRequiredService<IInterface5>();
        IInterface6 instance6 = provider.GetRequiredService<IInterface6>();
        ConcreteType3 instance3_1 = provider.GetRequiredService<ConcreteType3>();

        ReferenceEquals(instance5, instance3_1).Should().BeTrue();
        ReferenceEquals(instance5, instance6).Should().BeTrue();
        ReferenceEquals(instance6, instance3_1).Should().BeTrue();

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        IInterface5 instance1_2 = provider1.GetRequiredService<IInterface5>();
        IInterface6 instance6_2 = provider1.GetRequiredService<IInterface6>();
        ConcreteType3 instance3_2 = provider1.GetRequiredService<ConcreteType3>();

        object[] all = [instance5, instance6, instance3_1, instance1_2, instance6_2, instance3_2];
        all.Distinct().Count().Should().Be(1);
    }

    [Fact]
    public void AutoRegisterType_WithSingletonLifeTime_RegistersTypeOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface7? interface7 = provider.GetService<IInterface7>();
        IInterface8? interface8 = provider.GetService<IInterface8>();
        ConcreteType4 service = provider.GetRequiredService<ConcreteType4>();

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        ConcreteType4 service2 = provider1.GetRequiredService<ConcreteType4>();

        interface7.Should().BeNull();
        interface8.Should().BeNull();
        service.Should().Be(service2);
    }

    [Fact]
    public void AutoRegisterType_WithScopedLifeTime_RegistersTypeOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface9? interface7 = provider.GetService<IInterface9>();
        IInterface10? interface8 = provider.GetService<IInterface10>();
        ConcreteType5 service = provider.GetRequiredService<ConcreteType5>();
        ConcreteType5 service_1 = provider.GetRequiredService<ConcreteType5>();

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        ConcreteType5 service2 = provider1.GetRequiredService<ConcreteType5>();

        interface7.Should().BeNull();
        interface8.Should().BeNull();
        service.Should().NotBe(service2);
        service.Should().Be(service_1);
    }

    [Fact]
    public void AutoRegisterType_WithTransientLifeTime_RegistersTypeOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface11? interface11 = provider.GetService<IInterface11>();
        IInterface12? interface12 = provider.GetService<IInterface12>();
        ConcreteType6 service = provider.GetRequiredService<ConcreteType6>();
        ConcreteType6 service_1 = provider.GetRequiredService<ConcreteType6>();

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        ConcreteType6 service2 = provider1.GetRequiredService<ConcreteType6>();

        interface11.Should().BeNull();
        interface12.Should().BeNull();
        service.Should().NotBe(service2);
        service.Should().NotBe(service_1);
    }

    [Fact]
    public void AutoRegisterInterfaces_WithTransientLifeTime_RegistersInterfacesOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface13 interface11 = provider.GetRequiredService<IInterface13>();
        IInterface14 interface12 = provider.GetRequiredService<IInterface14>();
        ConcreteType7? service = provider.GetService<ConcreteType7>();

        interface11.Should().NotBeNull();
        interface12.Should().NotBeNull();
        service.Should().BeNull();
    }

    [Fact]
    public void AutoRegisterInterfaces_WithSingletonLifeTime_RegistersInterfacesOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface15 interface15 = provider.GetRequiredService<IInterface15>();
        IInterface16 interface16 = provider.GetRequiredService<IInterface16>();
        ConcreteType8? service = provider.GetService<ConcreteType8>();

        interface15.Should().Be(interface16);
        service.Should().BeNull();
    }

    [Fact]
    public void AutoRegisterInterfaces_WithScopedLifeTime_RegistersInterfacesOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface17 interface1 = provider.GetRequiredService<IInterface17>();
        IInterface18 interface2 = provider.GetRequiredService<IInterface18>();
        ConcreteType9? service = provider.GetService<ConcreteType9>();

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        IInterface17 interface3 = provider1.GetRequiredService<IInterface17>();
        IInterface18 interface4 = provider1.GetRequiredService<IInterface18>();

        interface1.Should().Be(interface2);
        interface3.Should().Be(interface4);
        interface1.Should().NotBe(interface3);
        interface2.Should().NotBe(interface4);
        service.Should().BeNull();
    }

    [Fact]
    public void AutoRegisterInterfaces_WithExcludedAssemblies_DoesNotRegisterExcludedAssembliesInterfaces()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(
            new RegistratorConfiguration()
            {
                Assemblies = [typeof(ConcreteType).Assembly],
                ExcludedAssemblies =
                [
                    typeof(IRequestHandler<>).Assembly,
                    typeof(IMediator).Assembly,
                ],
            }
        );

        ServiceProvider provider = services.BuildServiceProvider();
        IRequestHandler<CreateUser>? handler = provider.GetService<IRequestHandler<CreateUser>>();

        handler.Should().BeNull();
    }

    [Fact]
    public void AutoRegisterKeyedInterfaces_WithScopedLifeTime_RegistersCorrectly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface19 interface1 = provider.GetRequiredKeyedService<IInterface19>("11");
        IInterface20 interface2 = provider.GetRequiredKeyedService<IInterface20>("11");

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        IInterface19 interface3 = provider1.GetRequiredKeyedService<IInterface19>("12");
        IInterface20 interface4 = provider1.GetRequiredKeyedService<IInterface20>("12");

        interface1.Should().Be(interface2);
        interface3.Should().Be(interface4);

        interface1.Should().NotBe(interface3);
        interface2.Should().NotBe(interface4);
    }

    [Fact]
    public void AutoRegisterKeyedInterfaces_WithSingletonLifeTime_RegistersCorrectly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface21 interface1 = provider.GetRequiredKeyedService<IInterface21>("13");
        IInterface22 interface2 = provider.GetRequiredKeyedService<IInterface22>("13");

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        IInterface21 interface3 = provider1.GetRequiredKeyedService<IInterface21>("14");
        IInterface22 interface4 = provider1.GetRequiredKeyedService<IInterface22>("14");

        interface1.Should().Be(interface2);
        interface3.Should().Be(interface4);

        interface1.Should().NotBe(interface3);
        interface2.Should().NotBe(interface4);
    }

    [Fact]
    public void AutoRegisterKeyedInterfaces_WithTransientLifeTime_RegistersCorrectly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface23 interface1 = provider.GetRequiredKeyedService<IInterface23>("15");
        IInterface24 interface2 = provider.GetRequiredKeyedService<IInterface24>("15");

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        IInterface23 interface3 = provider1.GetRequiredKeyedService<IInterface23>("16");
        IInterface24 interface4 = provider1.GetRequiredKeyedService<IInterface24>("16");

        interface1.Should().NotBe(interface2);
        interface3.Should().NotBe(interface4);

        interface1.Should().NotBe(interface3);
        interface2.Should().NotBe(interface4);
    }

    [Fact]
    public void AutoRegisterKeyedType_WithScopedLifeTime_RegistersTypeOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface25? interface7 = provider.GetKeyedService<IInterface25>("17");
        IInterface26? interface8 = provider.GetKeyedService<IInterface26>("18");
        ConcreteType17 service = provider.GetRequiredKeyedService<ConcreteType17>("17");
        ConcreteType17 service_1 = provider.GetRequiredKeyedService<ConcreteType17>("17");

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        ConcreteType18 service2 = provider1.GetRequiredKeyedService<ConcreteType18>("18");

        interface7.Should().BeNull();
        interface8.Should().BeNull();
        service.Should().NotBe(service2);
        service.Should().Be(service_1);
    }

    [Fact]
    public void AutoRegisterKeyedType_WithSingletonLifeTime_RegistersTypeOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface27? interface7 = provider.GetKeyedService<IInterface27>("19");
        IInterface28? interface8 = provider.GetKeyedService<IInterface28>("19");
        ConcreteType19 service1 = provider.GetRequiredKeyedService<ConcreteType19>("19");
        ConcreteType19 service2 = provider.GetRequiredKeyedService<ConcreteType19>("19");

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        ConcreteType19 service3 = provider1.GetRequiredKeyedService<ConcreteType19>("19");

        interface7.Should().BeNull();
        interface8.Should().BeNull();
        service1.Should().Be(service2);
        service1.Should().Be(service3);
    }

    [Fact]
    public void AutoRegisterKeyedType_WithTransientLifeTime_RegistersTypeOnly()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface29? interface7 = provider.GetKeyedService<IInterface29>("20");
        IInterface30? interface8 = provider.GetKeyedService<IInterface30>("20");
        ConcreteType20 service1 = provider.GetRequiredKeyedService<ConcreteType20>("20");
        ConcreteType20 service2 = provider.GetRequiredKeyedService<ConcreteType20>("20");

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        ConcreteType20 service3 = provider1.GetRequiredKeyedService<ConcreteType20>("20");

        interface7.Should().BeNull();
        interface8.Should().BeNull();
        service1.Should().NotBe(service2);
        service1.Should().NotBe(service3);
    }

    [Fact]
    public void AutoRegisterKeyedTypeAndInterfaces_WithScopedLifeTime_RegistersTypeAndInterfaces()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface31 instance1 = provider.GetRequiredKeyedService<IInterface31>("21");
        IInterface32 instance2 = provider.GetRequiredKeyedService<IInterface32>("21");
        ConcreteType21 instance3 = provider.GetRequiredKeyedService<ConcreteType21>("21");

        instance1.Should().Be(instance3);
        instance1.Should().Be(instance2);
        instance2.Should().Be(instance3);
    }

    [Fact]
    public void AutoRegisterKeyedTypeAndInterfaces_WithTransientLifeTime_RegistersTypeAndInterfaces()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface33 instance1 = provider.GetRequiredKeyedService<IInterface33>("22");
        IInterface34 instance2 = provider.GetRequiredKeyedService<IInterface34>("22");
        ConcreteType22 instance3 = provider.GetRequiredKeyedService<ConcreteType22>("22");

        instance1.Should().NotBe(instance3);
        instance1.Should().NotBe(instance2);
        instance2.Should().NotBe(instance3);
    }

    [Fact]
    public void AutoRegisterKeyedTypeAndInterfaces_WithSingletonLifeTime_RegistersTypeAndInterfaces()
    {
        ServiceCollection services = [];
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface35 instance1 = provider.GetRequiredKeyedService<IInterface35>("23");
        IInterface36 instance2 = provider.GetRequiredKeyedService<IInterface36>("23");
        ConcreteType23 instance3 = provider.GetRequiredKeyedService<ConcreteType23>("23");

        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        ConcreteType23 instance4 = provider1.GetRequiredKeyedService<ConcreteType23>("23");

        instance1.Should().Be(instance3);
        instance1.Should().Be(instance2);
        instance2.Should().Be(instance3);
        instance4.Should().Be(instance3);
    }
}

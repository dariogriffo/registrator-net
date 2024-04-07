// ReSharper disable InconsistentNaming
namespace Registrator.Net.Tests;

using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AutoRegisterTypeAndInterfaces_WithScopedLifeTime_RegistersTypeAndInterfaces()
    {
        ServiceCollection services = new();
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
        ServiceCollection services = new();
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
        ServiceCollection services = new();
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
        ServiceCollection services = new();
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
        ServiceCollection services = new();
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
        ServiceCollection services = new();
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
        ServiceCollection services = new();
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface13? interface11 = provider.GetRequiredService<IInterface13>();
        IInterface14? interface12 = provider.GetRequiredService<IInterface14>();
        ConcreteType7? service = provider.GetService<ConcreteType7>();


        interface11.Should().NotBeNull();
        interface12.Should().NotBeNull();
        service.Should().BeNull();
    }

    [Fact]
    public void AutoRegisterInterfaces_WithSingletonLifeTime_RegistersInterfacesOnly()
    {
        ServiceCollection services = new();
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface15? interface15 = provider.GetRequiredService<IInterface15>();
        IInterface16? interface16 = provider.GetRequiredService<IInterface16>();
        ConcreteType8? service = provider.GetService<ConcreteType8>();

        interface15.Should().Be(interface16);
        service.Should().BeNull();
    }

    [Fact]
    public void AutoRegisterInterfaces_WithScopedLifeTime_RegistersInterfacesOnly()
    {
        ServiceCollection services = new();
        services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);

        ServiceProvider provider = services.BuildServiceProvider();
        IInterface17? interface17 = provider.GetRequiredService<IInterface17>();
        IInterface18? interface18 = provider.GetRequiredService<IInterface18>();
        ConcreteType9? service = provider.GetService<ConcreteType9>();


        IServiceProvider provider1 = provider.CreateScope().ServiceProvider;
        IInterface17? interface17_1 = provider1.GetRequiredService<IInterface17>();
        IInterface18? interface18_1 = provider1.GetRequiredService<IInterface18>();

        interface17.Should().Be(interface18);
        interface18_1.Should().Be(interface18_1);
        interface17.Should().NotBe(interface17_1);
        interface18.Should().NotBe(interface18_1);
        service.Should().BeNull();
    }

    [Fact]
    public void AutoRegisterInterfaces_WithExcludedAssemblies_DoesNotRegisterExcludedAssembliesInterfaces()
    {
        ServiceCollection services = new();
        services.AutoRegisterTypesInAssemblies(new RegistratorConfiguration()
        {
            Assemblies = [typeof(ConcreteType).Assembly],
            ExcludedAssemblies = [typeof(IRequestHandler<>).Assembly]
        });

        ServiceProvider provider = services.BuildServiceProvider();
        IRequestHandler<CreateUser>? handler = provider.GetService<IRequestHandler<CreateUser>>();

        handler.Should().BeNull();
    }
}

using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace MyBenchmarks
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Registrator.Net;
    using Registrator.Net.Tests;

    [MemoryDiagnoser]
    public class Benchmarker
    {
        [Benchmark]
        public void ManualRegistration()
        {
            ServiceCollection services = [];
            services.AddTransient<IRequestHandler<CreateUser>>(sp =>
                sp.GetRequiredService<ConcreteType10>()
            );
            services.AddScoped<IInterface1>(sp => sp.GetRequiredService<ConcreteType>());
            services.AddScoped<IInterface2>(sp => sp.GetRequiredService<ConcreteType>());
            services.AddScoped<IInterface17>(sp => sp.GetRequiredService<ConcreteType9>());
            services.AddScoped<IInterface18>(sp => sp.GetRequiredService<ConcreteType9>());
            services.AddScoped<IInterface16>(sp => sp.GetRequiredService<ConcreteType8>());
            services.AddScoped<IInterface15>(sp => sp.GetRequiredService<ConcreteType8>());
            services.AddScoped<IInterface13>(sp => sp.GetRequiredService<ConcreteType7>());
            services.AddScoped<IInterface14>(sp => sp.GetRequiredService<ConcreteType7>());
            services.AddScoped<ConcreteType5>();
            services.AddSingleton<ConcreteType4>();
            services.AddTransient<ConcreteType4>();

            services.AddScoped<ConcreteType>();
            services.AddTransient<IInterface1>(sp => sp.GetRequiredService<ConcreteType>());
            services.AddTransient<IInterface2>(sp => sp.GetRequiredService<ConcreteType>());

            services.AddScoped<ConcreteType3>();
            services.AddTransient<IInterface5>(sp => sp.GetRequiredService<ConcreteType3>());
            services.AddTransient<IInterface6>(sp => sp.GetRequiredService<ConcreteType3>());

            services.AddScoped<ConcreteType2>();
            services.AddTransient<IInterface3>(sp => sp.GetRequiredService<ConcreteType2>());
            services.AddTransient<IInterface4>(sp => sp.GetRequiredService<ConcreteType2>());
        }

        [Benchmark]
        public void Registrator()
        {
            ServiceCollection services = [];
            services.AutoRegisterTypesInAssemblies(typeof(ConcreteType).Assembly);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarker>();
        }
    }
}

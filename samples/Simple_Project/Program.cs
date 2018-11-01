﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Simple_Project.Abstracts;
using Simple_Project.Services;
using Microsoft.Extensions.DependencyInjection.Intercepting;
using DI.Intercepting.Core.Implementation;
using Simple_Project.Models;

namespace Simple_Project
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            ConfigureServices();
            ISomeServiceForSomeModel1 someServiceForSomeModel1 = serviceProvider.GetService<ISomeServiceForSomeModel1>();
            someServiceForSomeModel1.AddSomeModel1(new SomeModel1());

            Console.ReadLine();
            /* Output:
                This info from middleware!
                Method AddSomeModel1 has started execution.
                SomeModel1 {"Name":null,"Count":0} was added
                Method AddSomeModel1 has finished execution and returned True.
                This info from middleware!
            */
        }

        static void ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddThroughInterceptorsPipeline(sc =>
            {
                sc
                .AddInvocationMiddleware((ctx, next) => {
                    Console.WriteLine("This info from middleware!");
                    next();
                    Console.WriteLine("This info from middleware!");
                })
                .AddProxyProvider(new InterceptorProviderServiceDescriptor(new SomeProxyProvider())) // Register interceptor
                .AddSingleton<ISomeServiceForSomeModel1, SomeServiceForSomeModel1>(); // Register service that you need to call through interceptor
            });

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}

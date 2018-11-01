using System;
using Microsoft.Extensions.DependencyInjection;
using Simple_Project.Abstracts;
using Simple_Project.Models;
using Simple_Project.Services;
using Microsoft.Extensions.DependencyInjection.Intercepting;
using DI.Intercepting.Logging.Core.Abstract;
using Microsoft.Extensions.DependencyInjection.Intercepting.Logging;
using Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation.DataAnnotation;
using Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation.FluentValidation;
using Microsoft.Extensions.DependencyInjection.Intercepting.Repeater;
using DI.Intercepting.Repeater.Abstract;

namespace Simple_Project
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            ConfigureServices();
            //new ProxyGenerator().CreateInterfaceProxyWithTarget<ISomeServiceForSomeModel1>(new SomeServiceForSomeModel1(), );
            //Console.ReadLine();
            try
            {
                var s = serviceProvider.GetService<ISomeServiceForSomeModel2>();

                //var sp = serviceProvider.GetService<ISomeServiceForSomeModel1>();
                //try
                //{
                //    sp.AddSomeModel1(new SomeModel1());
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e);
                //}

                //var r = s.Do();
                s.AddSomeModel2(new SomeModel2
                {
                    Path = "someemail@gmail.com",
                    Email = ""
                },
                    new SomeModel2
                    {
                        Path = "someemail@gmail.com",
                        Email = "someemail@gmail.com"
                    },
                    new SomeModel2
                    {
                        Path = "someemail@gmail.com",
                        Email = "someemail@gmail.com"
                    },
                    new SomeModel2
                    {
                        Path = "someemail@gmail.com",
                        Email = "someemail@gmail.com"
                    }, new SomeModel2
                    {
                        Path = "someemail@gmail.com",
                        Email = "someemail@gmail.com"
                    });

                //Should be with errors
            }
            catch (Exception e)
            {

            }
            Console.ReadLine();
        }

        static void ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IMethodInvocationLogger, Logger>();
            serviceCollection.AddSingleton<IRepeaterEventService, RepeaterEventService>();
            serviceCollection.AddThroughInterceptorsPipeline(cont =>
            {
                cont.AddInvocationMiddleware((ctx, next) =>
{
    Console.WriteLine("second pipeline");
    next();
}).AddFluentMethodArgsValidationProvider(typeof(Program).Assembly).AddSingleton<ISomeServiceForSomeModel1, SomeServiceForSomeModel1>();
            });
            serviceCollection.AddThroughInterceptorsPipeline(cont =>
            {
                cont
                    .AddInterceptionLogger()
                    .AddInvocationMiddleware((ctx, next) =>
                    {
                        Console.WriteLine("World");
                        var c = next;
                        next();
                        Console.WriteLine("World 2");
                        bool s = next == c;
                        next();
                        Console.WriteLine("World 3");
                        s = next == c;

                        //var logger = ctx.ServiceProvider.GetService<ILogger>();

                        //try
                        //{
                        //    next();
                        //}
                        //catch (Exception e)
                        //{
                        //    Console.ForegroundColor = ConsoleColor.Cyan;
                        //    Console.WriteLine(e);
                        //    Console.ResetColor();

                        //    logger.Log(LogLevel.Critical, new EventId(0), e, null);
                        //    throw e;
                        //}

                        //Console.WriteLine("from middleware");
                    })
                    .AddDataAnnotationMethodArgsValidationProvider()
                    .AddRepeater()
                    .AddFluentMethodArgsValidationProvider()

                    .AddSingleton<IDataAnnotationSampleService, DataAnnotationSampleService>()
                    .AddSingleton<ISomeServiceForSomeModel2, SomeServiceForSomeModel2>();
            });

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}

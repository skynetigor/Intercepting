# Intercepting
Adding supporting of  intercepting for Microsoft.Extension.DependencyInjection and MethodArgsValidation.

As interceptors is used Castle.DynamicProxy Interceptors.

# DI.Intercepting.Core
In order to create service through interceptor you need to register it in ServiceCollection as it's shown below:

Your service:
```c#
using Simple_Project.Models;

namespace Simple_Project.Services
{
    public class SomeServiceForSomeModel1 : ISomeServiceForSomeModel1
    {
        public bool AddSomeModel1(SomeModel1 someModel1)
        {
            Console.WriteLine($"SomeModel1 {JsonConvert.SerializeObject(someModel1)} was added");
            return true;
        }
    }
}
```
Your interceptor:
```c#
using System;
using DI.Intercepting.Core.Abstract;

namespace Simple_Project
{
    public class SomeProxyProvider : IInterceptingProvider
    {
        public void Intercept(IInvocationContext context, InvocationDelegate next)
        {
            WriteMessage($"Method {context.ServiceMethodInfo.Name} has started execution.");
            next();
            WriteMessage($"Method {context.ServiceMethodInfo.Name} has finished execution and returned { context.ExecuteTargetMethod() }.");
        }

        private void WriteMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

```
Program:
```c#
using System;
using Microsoft.Extensions.DependencyInjection;
using Simple_Project.Abstracts;
using Simple_Project.Services;
using Microsoft.Extensions.DependencyInjection.Intercepting;
using DI.Intercepting.Core.Implementation;

namespace Simple_Project
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            ConfigureServices();
            ISomeServiceForSomeModel1 someServiceForSomeModel1 = serviceProvider.GetService<ISomeServiceForSomeModel1>();
            someServiceForSomeModel1.AddSomeModel1(new Models.SomeModel1());

            Console.ReadLine();
            /* Output:
                Method AddSomeModel1 has started execution.
                SomeModel1 {"Name":null,"Count":0} was added
                Method AddSomeModel1 has finished execution and returned True. 
            */
        }

        static void ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddThroughInterceptorsPipeline(sc =>
            {
                sc
                .AddProxyProvider(new InterceptorProviderServiceDescriptor(new SomeProxyProvider())) // Register interceptor
                .AddSingleton<ISomeServiceForSomeModel1, SomeServiceForSomeModel1>(); // Register service that you need to call through interceptor
            });

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}

```
# DI.Intercepting.MethodArgsValidation.Core
Adds basic logic for validation of method arguments through interceptor. If any parameter doesn't correspond to validation rules MethodArgsValidationException exception will be thrown.

We still have the same service that is shown above:
```c#
using Simple_Project.Models;

namespace Simple_Project.Services
{
    public class SomeServiceForSomeModel1 : ISomeServiceForSomeModel1
    {
        public bool AddSomeModel1(SomeModel1 someModel1)
        {
            Console.WriteLine($"SomeModel1 {JsonConvert.SerializeObject(someModel1)} was added");
            return true;
        }
    }
}
```

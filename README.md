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
Adds basic logic for validation of method arguments through interceptor by using validation provider that should implement IMethodArgsValidationProvider. If any parameter doesn't correspond to validation rules MethodArgsValidationException exception will be thrown.

# DI.Intercepting.MethodArgsValidation.DataAnnotation
Implementation of IMethodArgsValidationProvider that uses DataAnnotation in order to validate method arguments.

# DI.Intercepting.MethodArgsValidation.FluentValidation
Implementation of IMethodArgsValidationProvider that uses FluentValidation in order to validate method arguments.

Example of method args validation is accesed in MethodArgsValidationSample
Service:
```c#
using System;
using MethodArgsValidationSample.Abstracts;
using MethodArgsValidationSample.Models;
using Newtonsoft.Json;

namespace MethodArgsValidationSample.Services
{
    public class DataAnnotationSampleService : IDataAnnotationSampleService
    {
        public void Add(DataAnnotationSampleModel model)
        {
            Console.WriteLine($"SomeModel1 {JsonConvert.SerializeObject(model)} was added");
        }

        public DataAnnotationSampleModel Remove(DataAnnotationSampleModel model)
        {
            Console.WriteLine($"SomeModel1 {JsonConvert.SerializeObject(model)} was removed");
            return model;
        }
    }
}
```
Model:
```c#
using System.ComponentModel.DataAnnotations;

namespace MethodArgsValidationSample.Models
{
    public class DataAnnotationSampleModel
    {
        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Range(10, 20)]
        public int Age { get; set; }
    }
}
```
Program:
```c#
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Intercepting;
using MethodArgsValidationSample.Abstracts;
using MethodArgsValidationSample.Services;
using MethodArgsValidationSample.Models;
using Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation.DataAnnotation;
using DI.Intercepting.MethodArgsValidation.Core;

namespace MethodArgsValidationSample
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            ConfigureServices();
            IDataAnnotationSampleService someServiceForSomeModel1 = serviceProvider
                                            .GetService<IDataAnnotationSampleService>();

            try
            {
                someServiceForSomeModel1.Add(new DataAnnotationSampleModel());
            }
            catch (MethodArgsValidationException e)
            {
                Console.WriteLine(e.Message);
                /* Error:
                 An error has occured in "Add" method of service "IDataAnnotationSampleService" upon an attempt to validate arguments.

                    Arguments that was not passed validation:

                        model (DataAnnotationSampleModel) - Validation failed :
                            Name:
                                The Name field is required.
                            Address:
                                The Address field is required.
                            Age:
                                The field Age must be between 10 and 20.
                */
            }
            Console.ReadLine();
            try
            {
                someServiceForSomeModel1.Add(new DataAnnotationSampleModel { Name = "1234567890", Address = "SomeAdress", Age = 15 });
            }
            catch (MethodArgsValidationException e)
            {
                Console.WriteLine(e.Message);
                /* Success:
                 SomeModel1 {"Name":"1234567890","Surname":null,"Address":"SomeAdress","Age":15} was added
                */
            }

            Console.ReadLine();

        }

        static void ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddThroughInterceptorsPipeline(sc =>
            {
                sc
                .AddDataAnnotationMethodArgsValidationProvider() // Register DataAnnotationMethodArgsValidationProvider in pipeline that will be validate method arguments
                .AddSingleton<IDataAnnotationSampleService, DataAnnotationSampleService>(); // Register service that you need to call through interceptor
            });

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}

```

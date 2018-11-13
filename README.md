# Intercepting
Adding supporting of  intercepting for Microsoft.Extension.DependencyInjection and MethodArgsValidation.

As interceptors is used Castle.DynamicProxy Interceptors.

# DI.Intercepting.Core
In order to create service through interceptor you need to register it in ServiceCollection.

<details><summary>Example code</summary>
<p>

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
using Simple_Project.Models;
using DI.Intercepting.Core.Extensions;

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
                .AddInvocationMiddleware((ctx, next) =>
                {
                    Console.WriteLine("This info from middleware before method execution!");
                    next();
                    Console.WriteLine("This info from middleware after method execution!");

                }) // Register middleware
                .AddSingleton(new SomeProxyProvider()); // Register interceptor as singleton
            })
            .AddSingleton<ISomeServiceForSomeModel1, SomeServiceForSomeModel1>(); // Register service that you need to call through interceptor

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}

```
</p>
</details>

# DI.Intercepting.MethodArgsValidation.Core
Adds basic logic for validation of method arguments through interceptor by using validation provider that should implement IMethodArgsValidationProvider. If any parameter doesn't correspond to validation rules MethodArgsValidationException exception will be thrown.

# DI.Intercepting.MethodArgsValidation.FluentValidation
Implementation of IMethodArgsValidationProvider that uses DataAnnotation in order to validate method arguments.

# DI.Intercepting.MethodArgsValidation.DataAnnotation
Implementation of IMethodArgsValidationProvider that uses FluentValidation in order to validate method arguments.

<details><summary>Example code</summary>
<p>
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
using Microsoft.Extensions.DependencyInjection.Intercepting.Logging;
using DI.Intercepting.Logging.Core.Abstract;

namespace MethodArgsValidationSample
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            ConfigureServices();
            IDataAnnotationSampleService someServiceForSomeModel1 = serviceProvider.GetService<IDataAnnotationSampleService>();

            try
            {
                someServiceForSomeModel1.Add(new DataAnnotationSampleModel());
            }
            catch (MethodArgsValidationException e)
            {
                /* Error:
                INFO FROM LOGGER: Invocation of "Add" method of "IDataAnnotationSampleService" service has started with following parameters:

                    model: {"Name":null,"Surname":null,"Address":null,"Age":0}

                INFO FROM LOGGER: An error has occured in "Add" method of service "IDataAnnotationSampleService" upon an attempt to validate arguments.

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
            
                someServiceForSomeModel1.Add(new DataAnnotationSampleModel { Name = "1234567890", Address = "SomeAdress", Age = 15 });
            
                /* Success:
                INFO FROM LOGGER: Invocation of "Add" method of "IDataAnnotationSampleService" service has started with following parameters:

                    model: {"Name":"1234567890","Surname":null,"Address":"SomeAdress","Age":15}

                    SomeModel1 {"Name":"1234567890","Surname":null,"Address":"SomeAdress","Age":15} was added
                INFO FROM LOGGER: Invocation of "Add" method of "IDataAnnotationSampleService" service has finished invocation and returned null
                */

            Console.ReadLine();
        }

        static void ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IMethodInvocationLogger, Logger>();
            serviceCollection.AddThroughInterceptorsPipeline(sc =>
            {
                sc
                .AddInterceptionLogger()
                .AddDataAnnotationMethodArgsValidationProvider(); // Register DataAnnotationMethodArgsValidationProvider in pipeline that will be validate method arguments
            })
            .AddSingleton<IDataAnnotationSampleService, DataAnnotationSampleService>(); // Register service that you need to call through interceptor

            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
```
</p>
</details>


All these samples are available [here](https://github.com/skynetigor/Intercepting/tree/master/samples)

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

    Info from logger: An error has occured in "Add" method of service "IDataAnnotationSampleService" upon an attempt to validate arguments.

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

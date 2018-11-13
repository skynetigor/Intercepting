using DI.Intercepting.Core.Abstract;
using DI.ValidationInterceptor.Adapters.DataAnnotation;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation.DataAnnotation
{
    public static class DataAnnotationsMethodArgsValidatorExtensions
    {
        /// <summary>
        /// The method that's adding DataAnnotation method args validation Provider into invocation pipeline
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection</param>
        /// <returns>ProxyProvidersServiceCollection</returns>
        public static IInterceptorsCollection AddDataAnnotationMethodArgsValidationProvider(this IInterceptorsCollection serviceCollection)
        {
            serviceCollection.AddMethodArgsValidationProvider(new DataAnnotationsMethodArgsValidationProvider());
            return serviceCollection;
        }
    }
}
using System.Reflection;
using DI.Intercepting.Core.Abstract;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation.FluentValidation
{
    /// <summary>
    /// The method that's adding FluentValidation method args validation Provider into invocation pipeline
    /// </summary>
    /// <param name="serviceCollection">ServiceCollection</param>
    /// <returns>ProxyProvidersServiceCollection</returns>
    public static class FluentValidationMethodArgsValidatorExtensions
    {
        /// <summary>
        /// Adds FluentValidationMethodArgs to invocation pipeline and use rules from assembly that was invocked this method
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection</param>
        /// <returns>ServiceCollection</returns>
        public static IInterceptorsCollection AddFluentMethodArgsValidationProvider(this IInterceptorsCollection serviceCollection)
        {
            serviceCollection.AddMethodArgsValidationProvider(new FluentValidationMethodArgsValidationProvider(Assembly.GetCallingAssembly()));
            return serviceCollection;
        }

        /// <summary>
        /// Adds FluentValidationMethodArgs to invocation pipeline
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection</param>
        /// <returns>ServiceCollection</returns>
        public static IInterceptorsCollection AddFluentMethodArgsValidationProvider(this IInterceptorsCollection serviceCollection, Assembly assemblyWithRules)
        {
            serviceCollection.AddMethodArgsValidationProvider(new FluentValidationMethodArgsValidationProvider(assemblyWithRules));
            return serviceCollection;
        }
    }
}

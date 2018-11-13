using DI.Intercepting.Core.Abstract;
using DI.Intercepting.MethodArgsValidation.Core.Abstracts;
using DI.Intercepting.MethodArgsValidation.Core.Internal;
using DI.Intercepting.Core.Extensions;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation
{
    public static class ProxyProvidersServiceCollectionExtensions
    {
        /// <summary>
        /// The method that is adding method arguments validation interceptor into pipeline
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection</param>
        /// <param name="provider">MethodArgsValidationProvider</param>
        /// <returns>ServiceCollection</returns>
        public static IInterceptorsCollection AddMethodArgsValidationProvider(this IInterceptorsCollection serviceCollection, IMethodArgsValidationProvider provider)
        {
            serviceCollection.AddSingleton(new MethodValidationInterceptor(provider));
            return serviceCollection;
        }
    }
}

using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Implementation;
using DI.Intercepting.MethodArgsValidation.Core.Abstracts;
using DI.Intercepting.MethodArgsValidation.Core.Internal;

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
        public static IInterceptorsPipelineServiceCollection AddMethodArgsValidationProvider(this IInterceptorsPipelineServiceCollection serviceCollection, IMethodArgsValidationProvider provider)
        {
            serviceCollection.AddProxyProvider(new InterceptorProviderServiceDescriptor(new MethodValidationInterceptor(provider)));
            return serviceCollection;
        }
    }
}

using System;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Implementation.Internal;

namespace Microsoft.Extensions.DependencyInjection.Intercepting
{
    public static class InterceptorsExtensions
    {
        public static IServiceCollection AddThroughInterceptorsPipeline(this IServiceCollection serviceCollection, Action<IInterceptorsCollection> configAction)
        {
            ProxyContainer proxyContainer = new ProxyContainer();
            InterceptorsCollections interceptorProviderServiceDescriptors = new InterceptorsCollections(proxyContainer);

            configAction(interceptorProviderServiceDescriptors);

            return new CustomServiceCollection(serviceCollection, proxyContainer);
        }
    }
}
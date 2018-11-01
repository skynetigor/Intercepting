using System;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Implementation;
using DI.Intercepting.Core.Implementation.Internal;

namespace Microsoft.Extensions.DependencyInjection.Intercepting
{
    public static class InterceptorsExtensions
    {
        public static IServiceCollection AddThroughInterceptorsPipeline(this IServiceCollection serviceCollection, Action<IInterceptorsPipelineServiceCollection> configAction)
        {
            configAction(new CustomServiceCollection(serviceCollection));
            return serviceCollection;
        }

        public static IInterceptorsPipelineServiceCollection AddInvocationMiddleware(
            this IInterceptorsPipelineServiceCollection serviceCollection,
            Action<IInvocationContext, InvocationDelegate> middelware)
        {
            return serviceCollection.AddProxyProvider(
                 new InterceptorProviderServiceDescriptor(sp => new MiddlewareProxyInterceptingProvider(middelware), ServiceLifetime.Transient));
        }
    }
}
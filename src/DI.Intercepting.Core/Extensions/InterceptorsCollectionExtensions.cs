using System;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Enums;
using DI.Intercepting.Core.Implementation;
using DI.Intercepting.Core.Implementation.Internal;

namespace DI.Intercepting.Core.Extensions
{
    public static class InterceptorsCollectionExtensions
    {
        public static IInterceptorsCollection Add(this IInterceptorsCollection sc, Func<IServiceProvider, IInterceptingProvider> item, InterceptorLifeTime serviceLifetime)
        {
            sc.Add(new InterceptorProviderServiceDescriptor(item, serviceLifetime));
            return sc;
        }

        public static IInterceptorsCollection Add(this IInterceptorsCollection sc, Type interceptingProviderType, InterceptorLifeTime serviceLifetime)
        {
            sc.Add(new InterceptorProviderServiceDescriptor(interceptingProviderType, serviceLifetime));
            return sc;
        }

        public static IInterceptorsCollection AddSingleton(this IInterceptorsCollection sc, IInterceptingProvider factory)
        {
            sc.Add(new InterceptorProviderServiceDescriptor(factory));
            return sc;
        }

        public static IInterceptorsCollection AddSingleton(this IInterceptorsCollection sc, Func<IServiceProvider, IInterceptingProvider> factory)
        {
            sc.Add(new InterceptorProviderServiceDescriptor(factory));
            return sc;
        }

        public static IInterceptorsCollection Add(this IInterceptorsCollection sc, Type interceptingProviderType)
        {
            sc.Add(new InterceptorProviderServiceDescriptor(interceptingProviderType));
            return sc;
        }

        public static IInterceptorsCollection AddInvocationMiddleware(
            this IInterceptorsCollection serviceCollection,
            Action<IInvocationContext, InvocationDelegate> middelware)
        {
            return serviceCollection.Add(sp => new MiddlewareProxyInterceptingProvider(middelware), InterceptorLifeTime.Transient);
        }
    }
}

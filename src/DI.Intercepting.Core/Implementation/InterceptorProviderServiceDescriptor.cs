using System;
using DI.Intercepting.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Implementation
{
    public class InterceptorProviderServiceDescriptor
    {
        public InterceptorProviderServiceDescriptor(Func<IServiceProvider, IInterceptingProvider> implementationTypeFactory, ServiceLifetime serviceLifetime)
        {
            this.InterceptorFactory = implementationTypeFactory;
            Lifetime = serviceLifetime;
        }

        public InterceptorProviderServiceDescriptor(Type interceptorType, ServiceLifetime serviceLifetime)
            : this(sp => (IInterceptingProvider) ActivatorUtilities.CreateInstance(sp, interceptorType), serviceLifetime)
        {

        }

        public InterceptorProviderServiceDescriptor(IInterceptingProvider provider)
            : this(sp => provider, ServiceLifetime.Singleton)
        {

        }

        public Func<IServiceProvider, IInterceptingProvider> InterceptorFactory { get; }

        public ServiceLifetime Lifetime { get; }

        public static implicit operator ServiceDescriptor(InterceptorProviderServiceDescriptor serviceDescriptor)
        {
            return new ServiceDescriptor(typeof(IInterceptingProvider), sp => serviceDescriptor.InterceptorFactory(sp), serviceDescriptor.Lifetime);
        }
    }
}

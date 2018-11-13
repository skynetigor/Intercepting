using System;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Implementation
{
    public class InterceptorProviderServiceDescriptor
    {
        public InterceptorProviderServiceDescriptor(Func<IServiceProvider, IInterceptingProvider> implementationTypeFactory, InterceptorLifeTime interceptorLifeTime)
        {
            this.InterceptorFactory = implementationTypeFactory;
            Lifetime = interceptorLifeTime;
        }

        public InterceptorProviderServiceDescriptor(Type interceptorType, InterceptorLifeTime interceptorLifeTime)
            : this(sp => (IInterceptingProvider) ActivatorUtilities.CreateInstance(sp, interceptorType), interceptorLifeTime)
        {
            
        }

        public InterceptorProviderServiceDescriptor(IInterceptingProvider provider)
            : this(sp => provider, InterceptorLifeTime.Singleton)
        {

        }

        public InterceptorProviderServiceDescriptor(Type interceptorType)
            : this(sp => (IInterceptingProvider)ActivatorUtilities.CreateInstance(sp, interceptorType), InterceptorLifeTime.Singleton)
        {

        }

        public InterceptorProviderServiceDescriptor(Func<IServiceProvider, IInterceptingProvider> implementationTypeFactory):
            this(implementationTypeFactory, InterceptorLifeTime.Singleton)
        {
                
        }

        public Func<IServiceProvider, IInterceptingProvider> InterceptorFactory { get; }

        public InterceptorLifeTime Lifetime { get; }
    }
}

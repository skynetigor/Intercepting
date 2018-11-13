using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class InterceptorsCollections: AbstractCollection<InterceptorProviderServiceDescriptor>, IInterceptorsCollection
    {
        private readonly ProxyContainer _proxyContainer;

        public InterceptorsCollections(ProxyContainer proxyContainer)
        {
            _proxyContainer = proxyContainer;
        }

        public override void Add(InterceptorProviderServiceDescriptor interceptingProviderServiceDescriptor)
        {
            var lifeTime = interceptingProviderServiceDescriptor.Lifetime == InterceptorLifeTime.Transient ? ServiceLifetime.Transient : ServiceLifetime.Singleton;
            var serviceDescriptor = new ServiceDescriptor(typeof(IInterceptingProvider), sp => interceptingProviderServiceDescriptor.InterceptorFactory(sp), lifeTime);
            _proxyContainer.InternalServiceCollection.Add(serviceDescriptor);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class CustomServiceCollection : AbstractCollection<ServiceDescriptor>, IServiceCollection
    {
        private readonly ProxyContainer _proxyContainer;

        protected IServiceCollection ExternalServiceCollection { get; }

        public CustomServiceCollection(IServiceCollection externalServiceCollection, ProxyContainer proxyContainer)
        {
            ExternalServiceCollection = externalServiceCollection;
            _proxyContainer = proxyContainer;
        }

        public override void Add(ServiceDescriptor item)
        {
            _proxyContainer.Build();

            if (item.ImplementationType != null)
            {
                ExternalServiceCollection.Add(new ServiceDescriptor(item.ServiceType, sp => _proxyContainer.ProxyGenerator.CreateInterfaceProxyWithoutTarget(item.ServiceType, new Interceptor(sp, _proxyContainer, item.ImplementationType)), item.Lifetime));
            }
            else
            {
                ExternalServiceCollection.Add(new ServiceDescriptor(item.ServiceType, sp => _proxyContainer.ProxyGenerator.CreateInterfaceProxyWithoutTarget(item.ServiceType, new Interceptor(sp, _proxyContainer, item.ServiceType)), item.Lifetime));
            }
        }
    }
}

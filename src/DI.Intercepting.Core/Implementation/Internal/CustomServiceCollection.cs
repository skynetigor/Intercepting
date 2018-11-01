using System.Collections;
using System.Collections.Generic;
using DI.Intercepting.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class CustomServiceCollection : IInterceptorsPipelineServiceCollection
    {
        private ProxyContainer _proxyContainer;

        protected IServiceCollection ExternalServiceCollection { get; }

        private ProxyContainer ProxyContainer => _proxyContainer ?? (_proxyContainer = new ProxyContainer());

        public CustomServiceCollection(IServiceCollection externalServiceCollection) : this(externalServiceCollection, new ProxyContainer())
        {

        }

        public CustomServiceCollection(IServiceCollection externalServiceCollection, ProxyContainer proxyContainer)
        {
            ExternalServiceCollection = externalServiceCollection;
            _proxyContainer = proxyContainer;
        }

        public ServiceDescriptor this[int index] { get => ExternalServiceCollection[index]; set => ExternalServiceCollection[index] = value; }

        public int Count => ExternalServiceCollection.Count;

        public bool IsReadOnly => ExternalServiceCollection.IsReadOnly;

        public void Add(ServiceDescriptor item)
        {
            ProxyContainer.Build();

            if (item.ImplementationType != null)
            {
                ExternalServiceCollection.Add(new ServiceDescriptor(item.ServiceType, sp => _proxyContainer.ProxyGenerator.CreateInterfaceProxyWithoutTarget(item.ServiceType, new Interceptor(sp, ProxyContainer, item.ImplementationType)), item.Lifetime));
            }
            else
            {
                ExternalServiceCollection.Add(new ServiceDescriptor(item.ServiceType, sp => _proxyContainer.ProxyGenerator.CreateInterfaceProxyWithoutTarget(item.ServiceType, new Interceptor(sp, ProxyContainer, item.ServiceType)), item.Lifetime));
            }
        }

        public IInterceptorsPipelineServiceCollection AddProxyProvider(InterceptorProviderServiceDescriptor serviceDescriptor)
        {
            ProxyContainer.InternalServiceCollection.Add(serviceDescriptor);

            return new CustomServiceCollection(ExternalServiceCollection, ProxyContainer);
        }

        public void Clear()
        {
            ExternalServiceCollection.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return ExternalServiceCollection.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            ExternalServiceCollection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return ExternalServiceCollection.GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return ExternalServiceCollection.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            ExternalServiceCollection.Insert(index, item);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return ExternalServiceCollection.Remove(item);
        }

        public void RemoveAt(int index)
        {
            ExternalServiceCollection.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ExternalServiceCollection.GetEnumerator();
        }
    }
}

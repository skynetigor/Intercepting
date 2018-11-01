//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.Extensions.DependencyInjection;

//namespace DI.Core.Implementation.Internal
//{
//    internal class InterceptorsPipelineCollection: IServiceCollection
//    {
//        private ProxyContainer _proxyContainer;
//        private readonly ServiceDescriptor[] _interceptorsServiceDescriptors;
//        private readonly List<ServiceDescriptor> _serviceDescriptors = new List<ServiceDescriptor>();

//        protected IServiceCollection ExternalServiceCollection { get; }

//        private ProxyContainer ProxyContainer => _proxyContainer ?? (_proxyContainer = new ProxyContainer(this._interceptorsServiceDescriptors));

//        public InterceptorsPipelineCollection(IServiceCollection externalServiceCollection,
//            IEnumerable<ServiceDescriptor> serviceDescriptorsList)
//        {
//            this._interceptorsServiceDescriptors = serviceDescriptorsList.ToArray();
//            ExternalServiceCollection = externalServiceCollection;
//        }

//        public ServiceDescriptor this[int index] { get => _serviceDescriptors[index]; set => Add(value); }

//        public int Count => _serviceDescriptors.Count;

//        public bool IsReadOnly => false;

//        public void Add(ServiceDescriptor item)
//        {
//            this._serviceDescriptors.Add(item);

//            if (item.ImplementationType != null)
//            {
//                ExternalServiceCollection.Add(new ServiceDescriptor(item.ServiceType, sp => ProxyContainer.ProxyGenerator.CreateInterfaceProxyWithoutTarget(item.ServiceType, new Interceptor(sp, ProxyContainer, item.ImplementationType)), item.Lifetime));
//            }
//            else
//            {
//                ExternalServiceCollection.Add(new ServiceDescriptor(item.ServiceType, sp => ProxyContainer.ProxyGenerator.CreateInterfaceProxyWithoutTarget(item.ServiceType, new Interceptor(sp, ProxyContainer, item.ServiceType)), item.Lifetime));
//            }
//        }

//        public void Clear()
//        {
//            _serviceDescriptors.Clear();
//        }

//        public bool Contains(ServiceDescriptor item)
//        {
//            return _serviceDescriptors.Contains(item);
//        }

//        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
//        {
//            _serviceDescriptors.CopyTo(array, arrayIndex);
//        }

//        public IEnumerator<ServiceDescriptor> GetEnumerator()
//        {
//            return _serviceDescriptors.GetEnumerator();
//        }

//        public int IndexOf(ServiceDescriptor item)
//        {
//            return _serviceDescriptors.IndexOf(item);
//        }

//        public void Insert(int index, ServiceDescriptor item)
//        {
//            _serviceDescriptors.Insert(index, item);
//        }

//        public bool Remove(ServiceDescriptor item)
//        {
//            return _serviceDescriptors.Remove(item);
//        }

//        public void RemoveAt(int index)
//        {
//            _serviceDescriptors.RemoveAt(index);
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return _serviceDescriptors.GetEnumerator();
//        }
//    }
//}

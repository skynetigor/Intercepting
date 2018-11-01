//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using DI.Core.Abstract;
//using Microsoft.Extensions.DependencyInjection;

//namespace DI.Core.Implementation.Internal
//{
//    internal class InterceptorsCollection : IInterceptorsPipelineServiceCollection
//    {
//        private ProxyContainer _proxyContainer;
//        private readonly IList<InterceptorProviderServiceDescriptor> _serviceDescriptorsList;

//        protected IServiceCollection ExternalServiceCollection { get; }

//        private ProxyContainer ProxyContainer => _proxyContainer ?? (_proxyContainer = new ProxyContainer(this._serviceDescriptorsList));

//        public InterceptorsCollection(IServiceCollection externalServiceCollection): this(externalServiceCollection, new List<InterceptorProviderServiceDescriptor>())
//        {
//        }

//        protected InterceptorsCollection(IServiceCollection externalServiceCollection,
//            IList<InterceptorProviderServiceDescriptor> serviceDescriptorsList)
//        {
//            this._serviceDescriptorsList = serviceDescriptorsList;
//            ExternalServiceCollection = externalServiceCollection;
//        }

//        public InterceptorProviderServiceDescriptor this[int index] { get => ExternalServiceCollection[index]; set => ExternalServiceCollection[index] = value; }

//        public int Count => ExternalServiceCollection.Count;

//        public bool IsReadOnly => ExternalServiceCollection.IsReadOnly;

//        public void Add(InterceptorProviderServiceDescriptor item)
//        {
            
//        }

//        public IInterceptorsPipelineServiceCollection AddProxyProvider(InterceptorProviderServiceDescriptor serviceDescriptor)
//        {
//            this._serviceDescriptorsList.Add(serviceDescriptor);

//            return new InterceptorsCollection(ExternalServiceCollection, _serviceDescriptorsList.ToList());
//        }

//        public void Clear()
//        {
//            ExternalServiceCollection.Clear();
//        }

//        public bool Contains(InterceptorProviderServiceDescriptor item)
//        {
//            return ExternalServiceCollection.Contains(item);
//        }

//        public void CopyTo(InterceptorProviderServiceDescriptor[] array, int arrayIndex)
//        {
//            ExternalServiceCollection.CopyTo(array, arrayIndex);
//        }

//        public IEnumerator<InterceptorProviderServiceDescriptor> GetEnumerator()
//        {
//            return ExternalServiceCollection.GetEnumerator();
//        }

//        public int IndexOf(InterceptorProviderServiceDescriptor item)
//        {
//            return ExternalServiceCollection.IndexOf(item);
//        }

//        public void Insert(int index, InterceptorProviderServiceDescriptor item)
//        {
//            ExternalServiceCollection.Insert(index, item);
//        }

//        public bool Remove(InterceptorProviderServiceDescriptor item)
//        {
//            return ExternalServiceCollection.Remove(item);
//        }

//        public void RemoveAt(int index)
//        {
//            ExternalServiceCollection.RemoveAt(index);
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ExternalServiceCollection.GetEnumerator();
//        }
//    }
//}

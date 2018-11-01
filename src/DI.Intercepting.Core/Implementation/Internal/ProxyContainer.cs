using System;
using Castle.DynamicProxy;
using DI.Intercepting.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class ProxyContainer: IDisposable
    {
        public ProxyContainer()
        {
            this.ProxyGenerator = new ProxyGenerator();
            this.InternalServiceCollection = new ServiceCollection();
        }

        public ProxyGenerator ProxyGenerator { get; private set; }

        public IServiceProvider InternalServiceProvider { get; private set; }

        public IServiceCollection InternalServiceCollection { get; set; }

        public void Build()
        {
            if (InternalServiceProvider == null)
            {
                ConfigureServices();
                this.InternalServiceProvider = InternalServiceCollection.BuildServiceProvider();
            }
        }

        private void ConfigureServices()
        {
            InternalServiceCollection.AddSingleton<IInterceptorsExecutor, InterceptorsExecutor>();
        }

        #region IDisposable Support
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {

                }

                ProxyGenerator = null;
                InternalServiceProvider = null;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

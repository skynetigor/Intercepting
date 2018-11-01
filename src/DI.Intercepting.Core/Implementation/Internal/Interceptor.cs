using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Attributes;
using DI.Intercepting.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class Interceptor : IInterceptor
    {
        private readonly IServiceProvider _externalServiceProvider;
        private readonly ProxyContainer _proxyContainer;
        private readonly Type _targetType;

        public Interceptor(
            IServiceProvider externalServiceProvider,
            ProxyContainer proxyContainer,
            Type targetType)
        {
            _externalServiceProvider = externalServiceProvider;
            _proxyContainer = proxyContainer;
            _targetType = targetType;
        }

        public void Intercept(IInvocation invocation)
        {
            var invocationContext = new InvocationContext(invocation, _targetType, _externalServiceProvider);

            if (!invocationContext.ImplementationMethodInfo.HasAttribute<ExcludeFromInterceptingAttribute>())
            {
                var interceptors = _proxyContainer.InternalServiceProvider.GetService<IEnumerable<IInterceptingProvider>>();
                _proxyContainer.InternalServiceProvider.GetService<IInterceptorsExecutor>().StartExecuting(invocationContext, interceptors);
            }

            invocationContext.ExecuteTargetMethod();
        }
    }
}

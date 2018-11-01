using System;
using DI.Intercepting.Core.Abstract;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class MiddlewareProxyInterceptingProvider: IInterceptingProvider
    {
        private readonly Action<IInvocationContext, InvocationDelegate> _middelware;

        public MiddlewareProxyInterceptingProvider(Action<IInvocationContext, InvocationDelegate> middelware)
        {
            _middelware = middelware;
        }

        public void Intercept(IInvocationContext context, InvocationDelegate next)
        {
            _middelware(context, next);
        }
    }
}

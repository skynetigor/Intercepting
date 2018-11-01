using System;

namespace DI.Intercepting.Core.Abstract
{
    public delegate void InvocationDelegate();

    public interface IInterceptingProvider
    {
        void Intercept(IInvocationContext context, InvocationDelegate next);
    }
}

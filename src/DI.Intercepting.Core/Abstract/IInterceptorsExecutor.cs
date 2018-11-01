using System.Collections.Generic;

namespace DI.Intercepting.Core.Abstract
{
    public interface IInterceptorsExecutor
    {
        void StartExecuting(IInvocationContext context, IEnumerable<IInterceptingProvider> interceptors);
    }
}

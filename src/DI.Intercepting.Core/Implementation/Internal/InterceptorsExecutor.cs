using System.Collections.Generic;
using System.Linq;
using DI.Intercepting.Core.Abstract;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class InterceptorsExecutor: IInterceptorsExecutor
    {
        public void StartExecuting(IInvocationContext context, IEnumerable<IInterceptingProvider> interceptors)
        {
            BuildPipeline(context, interceptors)?.Invoke();
        }

        private InvocationDelegate BuildPipeline(IInvocationContext context, IEnumerable<IInterceptingProvider> interceptors)
        {
            var interceptorsArray = interceptors.ToArray();

            if (interceptorsArray.Length > 0)
            {
                InvocationDelegate next = () => { };

                for (int i = interceptorsArray.Length - 1; i >= 0; i--)
                {
                    IInterceptingProvider interceptor = interceptorsArray[i];

                    if (interceptor != null)
                    {
                        InvocationDelegate invDel = next;
                        next = () =>
                        {
                            interceptor.Intercept(context, invDel);
                        };
                    }

                    if(i == 0)
                    {
                        return next;
                    }
                }
            }

            return null;
        }
    }
}

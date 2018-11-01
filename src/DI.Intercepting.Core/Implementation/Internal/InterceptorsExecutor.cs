using System.Collections.Generic;
using System.Linq;
using DI.Intercepting.Core.Abstract;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class InterceptorsExecutor: IInterceptorsExecutor
    {
        public void StartExecuting(IInvocationContext context, IEnumerable<IInterceptingProvider> interceptors)
        {
            var invocationPipeline = BuildPipeline(context, interceptors);

            foreach (var item in invocationPipeline)
            {
                item?.Invoke();
            }
        }

        private InvocationDelegate[] BuildPipeline(IInvocationContext context, IEnumerable<IInterceptingProvider> interceptors)
        {
            var interceptorsArray = interceptors.ToArray();
            InvocationDelegate[] invocationPipeline = new InvocationDelegate[interceptorsArray.Length + 1];

            if (interceptorsArray.Length > 0)
            {
                InvocationDelegate next = () => { };
                invocationPipeline[invocationPipeline.Length - 1] = next;

                for (int i = interceptorsArray.Length - 1; i >= 0; i--)
                {
                    IInterceptingProvider interceptor = interceptorsArray[i];

                    if (interceptor != null)
                    {
                        InvocationDelegate d = next;
                        next = () =>
                        {
                            interceptor.Intercept(context, d);
                        };

                        invocationPipeline[i] = next;
                    }
                }
            }

            return invocationPipeline;
        }
    }
}

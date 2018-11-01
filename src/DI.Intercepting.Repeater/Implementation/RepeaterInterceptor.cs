using System;
using System.Reflection;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Repeater.Abstract;

namespace DI.Intercepting.Repeater.Implementation
{
    internal class RepeaterInterceptor : IInterceptingProvider
    {
        private IRepeaterEventService repeaterEventService;

        public RepeaterInterceptor()
        {

        }

        public void Intercept(IInvocationContext context, InvocationDelegate next)
        {
            repeaterEventService = (IRepeaterEventService) context.ServiceProvider.GetService(typeof(IRepeaterEventService));

            var attribute = context.ImplementationMethodInfo.GetCustomAttribute<RepeatAttribute>(false);

            if (attribute != null)
            {
                for (int attempt = 1; attempt <= attribute.RetryCount; attempt++)
                {
                    InvokeEvent(r => r.StartNewAttempt(context.ImplementationMethodInfo, attempt));

                    try
                    {
                        next();
                        InvokeEvent(r => r.SuccessfullAttempt(context.ImplementationMethodInfo, attempt));
                        break;
                    }
                    catch(Exception ex)
                    {
                        InvokeEvent(r => r.InvalidAttempt(context.ImplementationMethodInfo, attempt, ex));

                        if (attempt == attribute.RetryCount)
                        {
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                next();
            }

            repeaterEventService = null;
        }

        private void InvokeEvent(Action<IRepeaterEventService> action)
        {
            if(repeaterEventService != null)
            {
                action?.Invoke(repeaterEventService);
            }
        }
    }
}

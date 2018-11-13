using System;
using DI.Intercepting.Core.Abstract;

namespace Simple_Project
{
    public class SomeProxyProvider : IInterceptingProvider
    {
        public void Intercept(IInvocationContext context, InvocationDelegate next)
        {
            WriteMessage($"Method {context.ServiceMethodInfo.Name} has started execution.");
            next(); // call next interceptor like in .net Core middleware
            WriteMessage($"Method {context.ServiceMethodInfo.Name} has finished execution and returned { context.ExecuteTargetMethod() }.");
        }

        private void WriteMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

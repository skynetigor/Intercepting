using System;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Logging.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Logging.Core.Implemantation.Internal
{
    public class LoggerInterceptionProvider : IInterceptingProvider
    {
        private readonly Func<IServiceProvider, IMethodInvocationLogger> _loggerFactory;

        public LoggerInterceptionProvider(Func<IServiceProvider, IMethodInvocationLogger> loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public LoggerInterceptionProvider(IMethodInvocationLogger logger) : this(sp => logger)
        {

        }

        public LoggerInterceptionProvider() : this(sp => sp.GetService<IMethodInvocationLogger>())
        {

        }

        public void Intercept(IInvocationContext context, InvocationDelegate next)
        {
            var logger = _loggerFactory(context.ServiceProvider);
            var invocationInfo = new InvocationInfo(context);

            try
            {
                logger.BeforeInvocation(invocationInfo);
                next();
                logger.AfterInvocation(invocationInfo);
            }
            catch (Exception e)
            {
                logger.LogException(invocationInfo, e);
                throw e;
            }
        }
    }
}

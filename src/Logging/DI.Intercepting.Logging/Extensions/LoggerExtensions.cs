using System;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Logging.Core.Abstract;
using DI.Intercepting.Logging.Core.Implemantation.Internal;
using DI.Intercepting.Core.Extensions;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.Logging
{
    public static class LoggerExtensions
    {
        public static IInterceptorsCollection AddInterceptionLogger(
            this IInterceptorsCollection serviceCollection, IMethodInvocationLogger logger)
        {
            return serviceCollection.AddSingleton(new LoggerInterceptionProvider(logger));
        }

        public static IInterceptorsCollection AddInterceptionLogger<TLogger>(
            this IInterceptorsCollection serviceCollection) where TLogger : IMethodInvocationLogger
        {
            return serviceCollection.AddSingleton(new LoggerInterceptionProvider(sp => sp.GetService<TLogger>()));
        }

        public static IInterceptorsCollection AddInterceptionLogger(
            this IInterceptorsCollection serviceCollection, Func<IServiceProvider, IMethodInvocationLogger> loggerFactory)
        {
            return serviceCollection.AddSingleton(new LoggerInterceptionProvider(loggerFactory));
        }

        public static IInterceptorsCollection AddInterceptionLogger(this IInterceptorsCollection serviceCollection)
        {
            return serviceCollection.AddSingleton(new LoggerInterceptionProvider());
        }
    }
}

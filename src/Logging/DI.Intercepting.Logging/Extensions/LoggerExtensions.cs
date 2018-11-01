using System;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Implementation;
using DI.Intercepting.Logging.Core.Abstract;
using DI.Intercepting.Logging.Core.Implemantation.Internal;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.Logging
{
    public static class LoggerExtensions
    {
        public static IInterceptorsPipelineServiceCollection AddInterceptionLogger(
            this IInterceptorsPipelineServiceCollection serviceCollection, IMethodInvocationLogger logger)
        {
            return serviceCollection.AddProxyProvider(
                new InterceptorProviderServiceDescriptor(new LoggerInterceptionProvider(logger)));
        }

        public static IInterceptorsPipelineServiceCollection AddInterceptionLogger<TLogger>(
            this IInterceptorsPipelineServiceCollection serviceCollection) where TLogger : IMethodInvocationLogger
        {
            return serviceCollection.AddProxyProvider(
                new InterceptorProviderServiceDescriptor(
                    new LoggerInterceptionProvider(sp => sp.GetService<TLogger>())));
        }

        public static IInterceptorsPipelineServiceCollection AddInterceptionLogger(
            this IInterceptorsPipelineServiceCollection serviceCollection, Func<IServiceProvider, IMethodInvocationLogger> loggerFactory)
        {
            return serviceCollection.AddProxyProvider(
                new InterceptorProviderServiceDescriptor(
                    new LoggerInterceptionProvider(loggerFactory)));
        }

        public static IInterceptorsPipelineServiceCollection AddInterceptionLogger(this IInterceptorsPipelineServiceCollection serviceCollection)
        {
            return serviceCollection.AddProxyProvider(
                            new InterceptorProviderServiceDescriptor(
                                new LoggerInterceptionProvider()));
        }
    }
}

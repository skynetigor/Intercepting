using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Implementation;
using DI.Intercepting.Repeater.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.Repeater
{
    public static class ServiceCollectionExtension
    {
        public static IInterceptorsPipelineServiceCollection AddRepeater(this IInterceptorsPipelineServiceCollection serviceCollection)
        {
            return serviceCollection.AddProxyProvider(new InterceptorProviderServiceDescriptor(typeof(RepeaterInterceptor), ServiceLifetime.Singleton));
        }
    }
}

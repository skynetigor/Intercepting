using DI.Intercepting.Core.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Abstract
{
    public interface IInterceptorsPipelineServiceCollection : IServiceCollection
    {
        /// <summary>
        /// The method that's adding interceptors into invocation pipeline
        /// </summary>
        /// <param name="serviceDescriptor">InterceptorProviderServiceDescriptor</param>
        /// <returns></returns>
        IInterceptorsPipelineServiceCollection AddProxyProvider(InterceptorProviderServiceDescriptor serviceDescriptor);
    }
}

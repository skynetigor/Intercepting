using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Enums;
using DI.Intercepting.Core.Extensions;
using DI.Intercepting.Repeater.Implementation;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.Repeater
{
    public static class ServiceCollectionExtension
    {
        public static IInterceptorsCollection AddRepeater(this IInterceptorsCollection serviceCollection)
        {
            return serviceCollection.Add(typeof(RepeaterInterceptor), InterceptorLifeTime.Singleton);
        }
    }
}
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Extensions;
using DI.Intercepting.Repeater.Implementation;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.Repeater
{
    public static class ServiceCollectionExtension
    {
        public static IInterceptorsCollection AddRepeater(this IInterceptorsCollection serviceCollection)
        {
            return serviceCollection.AddSingleton(typeof(RepeaterInterceptor));
        }
    }
}
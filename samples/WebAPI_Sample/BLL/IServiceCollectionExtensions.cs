using AutoMapper;
using Core.BLL;
using DI.Core.Extensions;
using DI.ValidationInterceptor.Adapters.FluentValidator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper();
            return serviceCollection.AddProxies(collection =>
                 {
                     collection.AddFluentMethodArgsValidationProvider(typeof(ServiceCollectionExtensions).Assembly)
                         .AddScoped<IAccountService, AccountService>();
                 }).AddScoped<ICustomerService, CustomerService>();
        }
    }
}

using System;
using BLL;
using DAL;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class AddServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
           return serviceCollection
               .AddBusinessLogicLayer()
               .AddDataAccessLayer();
        }
    }
}

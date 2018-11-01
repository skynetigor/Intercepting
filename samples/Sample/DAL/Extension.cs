using System;
using System.Collections.Generic;
using System.Text;
using Core.DAL;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class Extension
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection serviceCollection)
        {
           return serviceCollection.AddScoped<IGenericRepository<User>, UserRepository>();
        }
    }
}

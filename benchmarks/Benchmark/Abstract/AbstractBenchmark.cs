﻿using System;
using Benchmark.Benchmarks.CommonServices;
using DI.Intercepting.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Intercepting;
namespace Benchmark.Abstract
{
    public abstract class AbstractBenchmark<TService, TModel> : IBenchmark
        where TModel : class
        where TService : class, ISampleService<TModel>
    {
        private IServiceProvider _provider;

        protected abstract void Intercepting(IInterceptorsCollection sc);

        public void SwitchToIntercepting()
        {
            Configure(t => t.AddThroughInterceptorsPipeline(Intercepting).AddSingleton<ISampleService<TModel>, SampleService<TModel>>());
        }

        public void SwitchToDirectCalling()
        {
            Configure(cont => cont.AddSingleton<ISampleService<TModel>, TService>());
        }

        public void Start(int repeats)
        {
            ISampleService<TModel> service = _provider.GetService<ISampleService<TModel>>();
            for (int i = 0; i < repeats; i++)
            {
                service.Do(CreateModel(), CreateModel(), CreateModel(), CreateModel(), CreateModel());
            }
        }

        protected abstract TModel CreateModel();

        private void Configure(Action<IServiceCollection> action)
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            action(serviceCollection);

            _provider = serviceCollection.BuildServiceProvider();
        }
    }
}

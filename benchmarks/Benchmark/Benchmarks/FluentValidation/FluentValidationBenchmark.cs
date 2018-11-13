﻿using Benchmark.Abstract;
using Benchmark.Benchmarks.FluentValidation.Models;
using Benchmark.Benchmarks.FluentValidation.Services;
using DI.Intercepting.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Intercepting;
using Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation.FluentValidation;


namespace Benchmark.Benchmarks.FluentValidation
{
    class FluentValidationBenchmark
        : AbstractBenchmark<FluentValidationSampleService, FluentValidationSampleModel>
    {
        protected override FluentValidationSampleModel CreateModel()
        {
            return new FluentValidationSampleModel
            {
                Name = "name",
                Email = "some@email.com",
                Address = "London",
                Surname = "surname",
                Code = "0345"
            };
        }

        protected override IInterceptorsCollection Intercepting(IServiceCollection sc)
        {
            IInterceptorsCollection p = null;
            sc.AddThroughInterceptorsPipeline(c => p = c.AddFluentMethodArgsValidationProvider());
            return p;
        }
    }
}

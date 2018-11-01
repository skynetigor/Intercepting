using Benchmark.Abstract;
using Benchmark.Benchmarks.DataAnnotation.Models;
using Benchmark.Benchmarks.DataAnnotation.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation.DataAnnotation;
using Microsoft.Extensions.DependencyInjection.Intercepting;
using DI.Intercepting.Core.Abstract;

namespace Benchmark.Benchmarks.DataAnnotation
{
    class DataAnnotationBenchmark
        : AbstractBenchmark<DataAnnotationSampleService<DataAnnotationSampleModel>, DataAnnotationSampleModel>
    {
        protected override DataAnnotationSampleModel CreateModel()
        {
            return new DataAnnotationSampleModel
            {
                Name = "name",
                Email = "some@email.com",
                Address = "London",
                Surname = "surname",
                Code = "0345"
            };
        }

        protected override IInterceptorsPipelineServiceCollection Intercepting(IServiceCollection sc)
        {
            IInterceptorsPipelineServiceCollection p = null;
            sc.AddThroughInterceptorsPipeline(c => p = c.AddDataAnnotationMethodArgsValidationProvider());

            return p;
        }
    }
}

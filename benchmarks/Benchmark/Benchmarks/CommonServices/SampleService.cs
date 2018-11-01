using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Benchmark.Abstract;

namespace Benchmark.Benchmarks.CommonServices
{
    class SampleService<TModel>: ISampleService<TModel>
    {
        public void Do(TModel model1, TModel model2, TModel model3, TModel model4, TModel model5)
        {

        }
    }
}

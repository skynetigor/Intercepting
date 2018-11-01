using Benchmark.Abstract;

namespace Benchmark.Benchmarks.CommonServices
{
    public abstract class DirectCallingSampleService<TModel>: ISampleService<TModel>
    {
        public void Do(TModel model1, TModel model2, TModel model3, TModel model4, TModel model5)
        {
            Validate(model1);
            Validate(model2);
            Validate(model3);
            Validate(model4);
            Validate(model5);
        }

        protected abstract void Validate(TModel model);
    }
}

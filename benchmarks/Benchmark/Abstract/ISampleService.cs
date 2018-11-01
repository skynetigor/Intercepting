namespace Benchmark.Abstract
{
    public interface ISampleService<TModel>
    {
        //[ExcludeFromIntercepting]
        //[ExcludeFromValidation]
        void Do(TModel model1, TModel model2, TModel model3, TModel model4, TModel model5);
    }
}

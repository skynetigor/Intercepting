using MethodArgsValidationSample.Models;

namespace MethodArgsValidationSample.Abstracts
{
    public interface IDataAnnotationSampleService
    {
        void Add(DataAnnotationSampleModel model);

        DataAnnotationSampleModel Remove(DataAnnotationSampleModel model);
    }
}

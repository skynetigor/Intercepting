using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Benchmark.Benchmarks.CommonServices;

namespace Benchmark.Benchmarks.DataAnnotation.Services
{
    class DataAnnotationSampleService<TModel> : DirectCallingSampleService<TModel>
    {
        protected override void Validate(TModel model)
        {
            var list = new List<ValidationResult>();
            var context = new ValidationContext(model);
            var isValid = Validator.TryValidateObject(model, context, list, true);
        }
    }
}

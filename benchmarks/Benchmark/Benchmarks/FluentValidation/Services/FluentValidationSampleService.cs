using Benchmark.Benchmarks.CommonServices;
using Benchmark.Benchmarks.FluentValidation.Models;
using Benchmark.Benchmarks.FluentValidation.Rules;
using FluentValidation;

namespace Benchmark.Benchmarks.FluentValidation.Services
{
    class FluentValidationSampleService : DirectCallingSampleService<FluentValidationSampleModel>
    {
        private readonly IValidator<FluentValidationSampleModel> _validator = new FluentValidationSampleModelRule();

        protected override void Validate(FluentValidationSampleModel model)
        {
            _validator.Validate(model);
        }
    }
}

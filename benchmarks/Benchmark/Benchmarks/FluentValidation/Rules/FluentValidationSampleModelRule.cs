using System;
using System.Collections.Generic;
using System.Text;
using Benchmark.Benchmarks.FluentValidation.Models;
using FluentValidation;

namespace Benchmark.Benchmarks.FluentValidation.Rules
{
    public class FluentValidationSampleModelRule: AbstractValidator<FluentValidationSampleModel>
    {
        public FluentValidationSampleModelRule()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Address).NotEmpty();
            RuleFor(t => t.Surname).NotEmpty();
            RuleFor(t => t.Code).NotEmpty();
            RuleFor(t => t.Email).NotEmpty();
        }
    }
}

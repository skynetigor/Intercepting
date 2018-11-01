using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Simple_Project.Models;

namespace Simple_Project.ValidatorRules
{
    class SomeModel1Rules : AbstractValidator<SomeModel1>
    {
        public SomeModel1Rules()
        {
            RuleFor(t => t.Name).Length(5).NotEmpty();
            RuleFor(t => t.Count).GreaterThan(10);
        }
    }

    class DataAnnotationSampleModelRules : AbstractValidator<DataAnnotationSampleModel>
    {
        public DataAnnotationSampleModelRules()
        {
            RuleFor(t => t.Name).Length(5).NotEmpty();
            //RuleFor(t => t.Count).GreaterThan(10);
        }
    }
}

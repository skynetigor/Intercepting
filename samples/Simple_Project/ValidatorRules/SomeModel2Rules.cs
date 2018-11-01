using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using Simple_Project.Models;

namespace Simple_Project.ValidatorRules
{
    class SomeModel2Rules : AbstractValidator<SomeModel2>
    {
        public SomeModel2Rules()
        {
            RuleFor(t => t.Path).NotEmpty().NotNull();
            RuleFor(t => t.Email).EmailAddress().NotEmpty().NotNull();
        }
    }
}

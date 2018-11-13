using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using FluentValidation;

namespace BLL.ValidationRules
{
    class RegistrationFormValidationRules: AbstractValidator<RegistrationForm>
    {
        public RegistrationFormValidationRules()
        {
            RuleFor(t => t.Password).NotEmpty().MaximumLength(5);
            RuleFor(t => t.ConfirmPassword)
                .MaximumLength(5)
                .When(t => t.Password != t.ConfirmPassword)
                .WithMessage("Passwords must be an equal");
            RuleFor(t => t.Email).EmailAddress().NotEmpty();
            RuleFor(t => t.UserName).NotEmpty().MinimumLength(5);
        }
    }
}

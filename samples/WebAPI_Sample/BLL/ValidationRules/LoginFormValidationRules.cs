using Core.Models;
using FluentValidation;

namespace BLL.ValidationRules
{
    class LoginFormValidationRules: AbstractValidator<LoginForm>
    {
        public LoginFormValidationRules()
        {
            RuleFor(t => t.Password).NotEmpty().MinimumLength(5);
            RuleFor(t => t.UserName).NotEmpty().MinimumLength(5);
        }
    }
}

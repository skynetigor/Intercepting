using Core.DAL.Models;
using FluentValidation;

namespace BLL.ValidationRules
{
    class CustomerValidator: AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(t => t.Email).NotEmpty().EmailAddress();
            RuleFor(t => t.FirstName).NotEmpty();
            RuleFor(t => t.LastName).NotEmpty();
        }
    }
}

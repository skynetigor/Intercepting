using System;
using System.Collections.Generic;
using System.Text;
using BLL.ValidationRules;
using Core.BLL;
using Core.DAL.Models;
using FluentValidation;
using FluentValidation.Results;

namespace BLL
{
    class CustomerService: ICustomerService
    {
        public IValidator<CustomerModel> _validator;

        public CustomerService()
        {
            _validator = new CustomerValidator();
        }

        public void CreateCustomer(CustomerModel customer)
        {
           ValidationResult result = _validator.Validate(customer);

            if (result.IsValid)
            {
                return;
            }

            throw new ValidationException(result.Errors);
        }

        public void RemoveCustomer(CustomerModel customer)
        {
            ValidationResult result = _validator.Validate(customer);

            if (result.IsValid)
            {
                return;
            }

            throw new ValidationException(result.Errors);
        }
    }
}

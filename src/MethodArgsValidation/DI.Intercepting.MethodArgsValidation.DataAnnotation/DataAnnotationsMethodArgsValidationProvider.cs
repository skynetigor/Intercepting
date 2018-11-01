using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DI.Intercepting.MethodArgsValidation.Core.Abstracts;
using DI.Intercepting.MethodArgsValidation.Core.Models;

namespace DI.ValidationInterceptor.Adapters.DataAnnotation
{
    internal class DataAnnotationsMethodArgsValidationProvider : AbstractMethodArgsValidationProvider
    {
        protected override ParameterValidationResult ValidateParameter(ParameterInfo parameter, object parameterValue)
        {
            var properties = parameter.ParameterType.GetProperties();

            var l = new List<PropertyValidationResult>();

            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var lists = new List<ValidationResult>();

                var isValid = !Validator.TryValidateProperty(property.GetValue(parameterValue),
                    new ValidationContext(parameterValue)
                    {
                        MemberName = property.Name
                    },
                    lists);

                if (isValid)
                {
                    l.Add(new PropertyValidationResult(property.Name, lists.Select(t => t.ErrorMessage)));
                }
            }

            return new ParameterValidationResult(parameter, l);
        }
    }
}

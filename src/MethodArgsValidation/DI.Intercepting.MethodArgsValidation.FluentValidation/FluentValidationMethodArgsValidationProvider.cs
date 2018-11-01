using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DI.Intercepting.MethodArgsValidation.Core.Abstracts;
using DI.Intercepting.MethodArgsValidation.Core.Models;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection.Intercepting.MethodArgsValidation.FluentValidation
{
    internal class FluentValidationMethodArgsValidationProvider : AbstractMethodArgsValidationProvider
    {
        private static IDictionary<Type, Type> ValidatorTypesDictionary { get; set; }

        private static IDictionary<Type, IValidator> _validatorInstances = new Dictionary<Type, IValidator>();

        private readonly object _forLocking = new object();

        public FluentValidationMethodArgsValidationProvider(Assembly assemblyWithRules)
        {
            ValidatorTypesDictionary = assemblyWithRules
                .GetTypes()
                .Where(IsTypeValidatorImplementation)
                .ToDictionary(KeySelector);
        }

        private bool IsTypeValidatorImplementation(Type type)
        {
            return type.GetInterfaces()
                       .Where(x => x.IsGenericType)
                       .Select(x => x.GetGenericTypeDefinition())
                       .Contains(typeof(IValidator<>)) && !type.IsAbstract;
        }

        private Type KeySelector(Type type)
        {
            return type.GetInterfaces()
                .FirstOrDefault(t => t.GetGenericTypeDefinition() == typeof(IValidator<>))
                .GetGenericArguments()[0];
        }

        public IValidator CreateValidator(Type modelType)
        {
            lock (_forLocking)
            {
                if (!_validatorInstances.TryGetValue(modelType, out var validatorInstance))
                {
                    if (ValidatorTypesDictionary.TryGetValue(modelType, out var validatorType))
                    {
                        validatorInstance = (IValidator)Activator.CreateInstance(validatorType);

                        _validatorInstances.Add(modelType, validatorInstance);
                    }
                }

                return validatorInstance;
            }
        }

        protected override ParameterValidationResult ValidateParameter(ParameterInfo parameter, object parameterValue)
        {
            var validator = this.CreateValidator(parameter.ParameterType);

            List<PropertyValidationResult> propertyValidationResult = null;

            if (validator != null)
            {
                var validationResult = validator.Validate(parameterValue);

                var errors = validationResult.Errors.ToArray();

                if (errors.Length > 0)
                {
                    propertyValidationResult = new List<PropertyValidationResult>();

                    foreach (var validationFailures in errors.GroupBy(t => t.PropertyName))
                    {
                        propertyValidationResult.Add(new PropertyValidationResult(validationFailures.Key,
                            validationFailures.Select(c => c.ErrorMessage)));
                    }
                }
            }

            return new ParameterValidationResult(parameter, propertyValidationResult);
        }
    }
}

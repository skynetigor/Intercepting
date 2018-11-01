using System.Collections.Generic;
using System.Reflection;
using DI.Intercepting.MethodArgsValidation.Core.Models;

namespace DI.Intercepting.MethodArgsValidation.Core.Abstracts
{
    /// <summary>
    /// The abstract class of MethodArgsValidationProvider with basic functionality for method arguments validation
    /// </summary>
    public abstract class AbstractMethodArgsValidationProvider: IMethodArgsValidationProvider
    {
        /// <inheritdoc cref="IMethodArgsValidationProvider"/>
        public IEnumerable<ParameterValidationResult> Validate(MethodInfo methodInfo, object[] args)
        {
            var result = new List<ParameterValidationResult>();

            var parameters = methodInfo.GetParameters();

            for (var i = 0; i < parameters.Length; i++)
            {
                var value = args[i];
                var parameter = parameters[i];

                if (!parameter.ParameterType.IsPrimitive && i < args.Length && args[i] != null)
                {
                    var parameterValidationResult = this.ValidateParameter(parameter, value);

                    if (!parameterValidationResult.IsValid)
                    {
                        result.Add(parameterValidationResult);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// The method that's validating method arguments
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <param name="parameterValue">ParameterValue</param>
        /// <returns>ParameterValidationResult</returns>
        protected abstract ParameterValidationResult ValidateParameter(ParameterInfo parameter, object parameterValue);
    }
}

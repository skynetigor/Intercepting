using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI.Intercepting.MethodArgsValidation.Core.Models
{
    /// <summary>
    /// The struct that describes parameter validation result
    /// </summary>
    public struct ParameterValidationResult
    {
        public ParameterValidationResult(ParameterInfo parameterInfo, IEnumerable<PropertyValidationResult> errors, string error)
        {
            ParameterInfo = parameterInfo;
            Errors = errors?.ToArray();
            IsValid = Errors == null || !Errors.Any();
            Error = error;
        }

        public ParameterValidationResult(ParameterInfo parameterInfo, IEnumerable<PropertyValidationResult> errors)
            : this(parameterInfo, errors, "Validation failed")
        {

        }

        public ParameterValidationResult(ParameterInfo parameterInfo, string error)
            : this(parameterInfo, new PropertyValidationResult[0], error) { }

        public ParameterInfo ParameterInfo { get; }

        public bool IsValid { get; }

        public string Error { get; }

        public IEnumerable<PropertyValidationResult> Errors { get; }
    }
}

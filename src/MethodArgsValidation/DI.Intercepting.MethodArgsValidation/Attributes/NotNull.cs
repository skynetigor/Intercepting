using System.Reflection;
using DI.Intercepting.MethodArgsValidation.Core.Abstracts;
using DI.Intercepting.MethodArgsValidation.Core.Models;

namespace DI.ValidationInterceptor.Core.Attributes
{
    class NotNull: AbstractParameterAttribute
    {
        public override ParameterValidationResult CheckParameter(ParameterInfo parameter, object parameterValue)
        {
            return new ParameterValidationResult(parameter, "Should not be null");
        }
    }
}

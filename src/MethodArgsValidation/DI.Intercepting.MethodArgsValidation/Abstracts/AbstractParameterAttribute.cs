using System;
using System.Reflection;
using DI.Intercepting.MethodArgsValidation.Core.Models;

namespace DI.Intercepting.MethodArgsValidation.Core.Abstracts
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public abstract class AbstractParameterAttribute: Attribute
    {
        public abstract ParameterValidationResult CheckParameter(ParameterInfo parameter, object parameterValue);
    }
}

using System.Collections.Generic;
using System.Reflection;
using DI.Intercepting.MethodArgsValidation.Core.Models;

namespace DI.Intercepting.MethodArgsValidation.Core.Abstracts
{
    public interface IMethodArgsValidationProvider
    {
        /// <summary>
        /// The method that's validating arguments of current method
        /// </summary>
        /// <param name="methodInfo">MethodInfo</param>
        /// <param name="args">Arguments</param>
        /// <returns></returns>
        IEnumerable<ParameterValidationResult> Validate(MethodInfo methodInfo, object[] args);
    }
}

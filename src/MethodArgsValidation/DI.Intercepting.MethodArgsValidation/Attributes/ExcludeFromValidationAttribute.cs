using System;

namespace DI.Intercepting.MethodArgsValidation.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface,
        AllowMultiple = false, Inherited = false)]
    public class ExcludeFromValidationAttribute: Attribute
    {
    }
}

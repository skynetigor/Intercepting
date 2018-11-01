using System;

namespace DI.Intercepting.Core.Attributes
{
    /// <summary>
    /// The attribute that indicates that method or whole object should be not intercepted
    /// </summary>

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ExcludeFromInterceptingAttribute: Attribute
    {
    }
}

using System;
using System.Reflection;

namespace DI.Intercepting.Logging.Core.Abstract
{
    public interface IInvocationInfo
    {
        object[] Argumets { get; }
        Type[] GenericArguments { get; }
        MethodInfo MethodInfo { get; }
        object ReturnedValue { get; }
    }
}

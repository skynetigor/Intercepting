using System;

namespace DI.Intercepting.Logging.Core.Abstract
{
    public interface IMethodInvocationLogger
    {
        void BeforeInvocation(IInvocationInfo invocationInfo);
        void AfterInvocation(IInvocationInfo invocationInfo);
        void LogException(IInvocationInfo invocationInfo, Exception exception);
    }
}

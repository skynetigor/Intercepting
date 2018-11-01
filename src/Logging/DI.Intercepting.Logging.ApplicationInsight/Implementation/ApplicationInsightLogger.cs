using System;
using DI.Intercepting.Logging.Core.Abstract;

namespace DI.Intercepting.Logging.ApplicationInsight.Implementation
{
    internal class ApplicationInsightLogger : IMethodInvocationLogger
    {
        //private readonly  telemetryClient = 
        public void AfterInvocation(IInvocationInfo invocationInfo)
        {
            throw new NotImplementedException();
        }

        public void BeforeInvocation(IInvocationInfo invocationInfo)
        {
            throw new NotImplementedException();
        }

        public void LogException(IInvocationInfo invocationInfo, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}

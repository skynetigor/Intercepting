using System;

namespace DI.Intercepting.Repeater.Implementation
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RepeatAttribute: Attribute
    {
        public RepeatAttribute(int retryCount)
        {
            RetryCount = retryCount;
        }

        public int RetryCount { get; }
    }
}

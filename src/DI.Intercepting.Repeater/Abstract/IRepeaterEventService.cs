using System;
using System.Reflection;

namespace DI.Intercepting.Repeater.Abstract
{
    public interface IRepeaterEventService
    {
        void StartNewAttempt(MethodInfo method, int attempt);
        void SuccessfullAttempt(MethodInfo method, int attempt);
        void InvalidAttempt(MethodInfo method, int attempt, Exception ex);
    }
}

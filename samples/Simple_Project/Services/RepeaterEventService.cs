using System;
using System.Reflection;
using DI.Intercepting.Repeater.Abstract;

namespace Simple_Project.Services
{
    public class RepeaterEventService : IRepeaterEventService
    {
        public void InvalidAttempt(MethodInfo method, int attempt, Exception ex)
        {
            Console.WriteLine($"Attempt number {attempt} was invalid");
        }

        public void StartNewAttempt(MethodInfo method, int attempt)
        {
            Console.WriteLine($"Attempt number {attempt} was started");
        }

        public void SuccessfullAttempt(MethodInfo method, int attempt)
        {
            Console.WriteLine($"Attempt number {attempt} was successfull");
        }
    }
}

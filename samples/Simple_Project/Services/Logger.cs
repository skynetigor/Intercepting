using System;
using System.Text;
using DI.Intercepting.Logging.Core.Abstract;
using Newtonsoft.Json;

namespace Simple_Project.Services
{
    class Logger : IMethodInvocationLogger
    {
        public void AfterInvocation(IInvocationInfo invocationInfo)
        {
            var method = invocationInfo.MethodInfo;
            var methodArguments = invocationInfo.Argumets;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"\n Invocation of \"{method.Name}\" method of \"{method.DeclaringType.Name}\" service has finished invocation and returned {JsonConvert.SerializeObject(invocationInfo.ReturnedValue)} ");
            Console.ResetColor();
        }

        public void BeforeInvocation(IInvocationInfo invocationInfo)
        {
            var method = invocationInfo.MethodInfo;
            var methodArguments = invocationInfo.Argumets;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INFO: \n");
            var builder = new StringBuilder($"\n Invocation of \"{method.Name}\" method of \"{method.DeclaringType.Name}\" service has started with following parameters: \n ");

            var paramss = method.GetParameters();

            for (var i = 0; i < paramss.Length; i++)
            {
                var p = paramss[i];
                var arg = methodArguments[i];
                builder.Append($"\n {p.Name}: {JsonConvert.SerializeObject(arg)}");
            }

            builder.Append("\n");
            Console.WriteLine(builder);
            Console.ResetColor();
        }

        public void LogException(IInvocationInfo invocationInfo, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("An error occured: \n");
            Console.WriteLine(exception.Message);

            Console.ResetColor();
        }
    }
}

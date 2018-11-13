using System;
using System.Text;
using DI.Intercepting.Logging.Core.Abstract;
using Newtonsoft.Json;

namespace MethodArgsValidationSample.Services
{
    class Logger : IMethodInvocationLogger
    {
        public void AfterInvocation(IInvocationInfo invocationInfo)
        {
            var method = invocationInfo.MethodInfo;
            var methodArguments = invocationInfo.Argumets;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"INFO FROM LOGGER: Invocation of \"{method.Name}\" method of \"{method.DeclaringType.Name}\" service has finished invocation and returned {JsonConvert.SerializeObject(invocationInfo.ReturnedValue)} ");
            Console.ResetColor();
        }

        public void BeforeInvocation(IInvocationInfo invocationInfo)
        {
            var method = invocationInfo.MethodInfo;
            var methodArguments = invocationInfo.Argumets;

            Console.ForegroundColor = ConsoleColor.Yellow;

            var builder = new StringBuilder($"Invocation of \"{method.Name}\" method of \"{method.DeclaringType.Name}\" service has started with following parameters: \n ");

            var paramss = method.GetParameters();

            for (var i = 0; i < paramss.Length; i++)
            {
                var p = paramss[i];
                var arg = methodArguments[i];
                builder.Append($"\n {p.Name}: {JsonConvert.SerializeObject(arg)}");
            }

            builder.Append("\n");
            Console.WriteLine($"INFO FROM LOGGER: {builder}" );
            Console.ResetColor();
        }

        public void LogException(IInvocationInfo invocationInfo, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Info from logger: {exception.Message} ");

            Console.ResetColor();
        }
    }
}

using System;
using System.Reflection;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Logging.Core.Abstract;

namespace DI.Intercepting.Logging.Core.Implemantation.Internal
{
    internal class InvocationInfo : IInvocationInfo
    {
        private readonly IInvocationContext _context;

        public InvocationInfo(IInvocationContext context)
        {
            this._context = context;
        }

        public object[] Argumets => _context.Arguments;

        public Type[] GenericArguments => _context.GenericArguments;

        public MethodInfo MethodInfo => _context.ServiceMethodInfo;

        public object ReturnedValue => _context.ExecuteTargetMethod();
    }
}

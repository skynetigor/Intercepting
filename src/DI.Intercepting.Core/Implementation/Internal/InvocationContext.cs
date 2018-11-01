using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using DI.Intercepting.Core.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Intercepting.Core.Implementation.Internal
{
    internal class InvocationContext : IInvocationContext
    {
        private readonly IInvocation _invocation;
        private readonly Type _targetType;

        private object _target;
        private MethodInfo _targetMethod;

        public InvocationContext(IInvocation invocation, Type targetType, IServiceProvider externalServiceProvider)
        {
            ServiceProvider = externalServiceProvider;
            _invocation = invocation;
            _targetType = targetType;
        }

        public IServiceProvider ServiceProvider { get; }

        public MethodInfo ServiceMethodInfo => _invocation.Method;

        public object Proxy => _invocation.Proxy;

        public object[] Arguments => _invocation.Arguments;

        public Type[] GenericArguments => _invocation.GenericArguments;

        public bool IsTargetLoaded { get; set; }

        public bool IsTargetMethodExecuted { get; private set; }

        public MethodInfo ImplementationMethodInfo
        {
            get
            {
                if (_targetMethod == null)
                {

                    _targetMethod = _targetType.GetMethods().FirstOrDefault(t => t.ToString() == _invocation.Method.ToString());
                }

                return _targetMethod;
            }
        }

        public object LoadTarget()
        {
            if (!IsTargetLoaded)
            {
                _target = ActivatorUtilities.CreateInstance(ServiceProvider, _targetType);
            }

            return _target;
        }

        public object ExecuteTargetMethod()
        {
            if (!IsTargetMethodExecuted)
            {
                _invocation.ReturnValue = _invocation.Method.Invoke(LoadTarget(), Arguments);
                IsTargetMethodExecuted = true;
            }

            return _invocation.ReturnValue;
        }

        public bool ChangeReturnValue(object value)
        {
            if (_invocation.ReturnValue != value)
            {
                _invocation.ReturnValue = value;
                return true;
            }

            return false;
        }
    }
}

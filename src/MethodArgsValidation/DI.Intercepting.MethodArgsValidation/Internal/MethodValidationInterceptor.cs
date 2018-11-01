using System.Linq;
using DI.Intercepting.Core.Abstract;
using DI.Intercepting.Core.Extensions;
using DI.Intercepting.MethodArgsValidation.Core.Abstracts;
using DI.Intercepting.MethodArgsValidation.Core.Attributes;

namespace DI.Intercepting.MethodArgsValidation.Core.Internal
{
    internal class MethodValidationInterceptor: IInterceptingProvider
    {
        private readonly IMethodArgsValidationProvider _provider;

        public MethodValidationInterceptor(IMethodArgsValidationProvider provider)
        {
            _provider = provider;
        }

        public void Intercept(IInvocationContext invocation, InvocationDelegate next)
        {
            if (!invocation.ServiceMethodInfo.HasAttribute<ExcludeFromValidationAttribute>(false) && invocation.Arguments.Any())
            {
                var parameterValidationResults= this._provider.Validate(invocation.ServiceMethodInfo, invocation.Arguments).ToArray();

                if (parameterValidationResults.Any())
                {
                    throw new MethodArgsValidationException(invocation.ServiceMethodInfo, parameterValidationResults);
                }

                next();
            }
        }
    }
}

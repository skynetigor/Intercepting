using System;
using System.Reflection;

namespace DI.Intercepting.Core.Abstract
{
    public interface IInvocationContext
    {
        /// <summary>
        /// Method arguments
        /// </summary>
        object[] Arguments { get; }

        /// <summary>
        /// Generic types method arguments
        /// </summary>
        Type[] GenericArguments { get; }

        /// <summary>
        /// The value which indicates that target was loaded or not
        /// </summary>
        bool IsTargetLoaded { get; }

        /// <summary>
        /// A method of service that was called
        /// </summary>
        MethodInfo ServiceMethodInfo { get; }

        /// <summary>
        /// A method of service implementation that was called
        /// </summary>
        MethodInfo ImplementationMethodInfo { get; }

        /// <summary>
        /// Proxy object
        /// </summary>
        object Proxy { get; }

        /// <summary>
        /// The value that indicates was target method executed or not
        /// </summary>
        bool IsTargetMethodExecuted { get; }

        /// <summary>
        /// Changes return value of target method
        /// </summary>
        /// <param name="value">value to change</param>
        /// <returns>is return value changed</returns>
        bool ChangeReturnValue(object value);

        /// <summary>
        /// Loads target
        /// </summary>
        /// <returns>Target</returns>
        object LoadTarget();

        /// <summary>
        /// Executes target method
        /// </summary>
        /// <returns>A value that target method is returning</returns>
        object ExecuteTargetMethod();

        /// <summary>
        /// ServiceProvider, that allows getting services from external ServiceProvider
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}
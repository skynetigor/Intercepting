using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DI.Intercepting.MethodArgsValidation.Core.Models;

namespace DI.Intercepting.MethodArgsValidation.Core
{
    /// <summary>
    /// The exception that will be thrown by validation interceptor if arguments isn't valid
    /// </summary>
    public class MethodArgsValidationException : Exception
    {
        private string message;

        public MethodInfo Method { get; }

        public IEnumerable<ParameterValidationResult> Errors { get; }

        public override string Message
        {
            get
            {
                if (message == null)
                {
                    message = BuildMessage(Method, Errors);
                }

                return message;
            }
        }

        /// <summary>
        /// Initializes a new instance of MethodArgsValidationException
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="errors">Errors</param>
        public MethodArgsValidationException(MethodInfo method, IEnumerable<ParameterValidationResult> errors)
        {
            Method = method;
            Errors = errors;
        }

        private string BuildMessage(MethodInfo method, IEnumerable<ParameterValidationResult> errors)
        {
            var stringBuilder = new StringBuilder($"An error has occured in \"{method.Name}\" method of service \"{method.DeclaringType.Name}\" upon an attempt to validate arguments. \n\n Arguments that was not passed validation: \n");

            foreach (var parameterValidationResult in errors)
            {
                stringBuilder.Append($"\n   {parameterValidationResult.ParameterInfo.Name} ({parameterValidationResult.ParameterInfo.ParameterType.Name}) - { parameterValidationResult.Error } :");
                foreach (var propertyValidationResult in parameterValidationResult.Errors)
                {
                    stringBuilder.Append($"\n     {propertyValidationResult.PropertyName}:");
                    foreach (var error in propertyValidationResult.Errors)
                    {
                        stringBuilder.Append($"\n         {error}");
                    }
                }

                stringBuilder.Append("\n");
            }

            stringBuilder.Append("\n");

            return stringBuilder.ToString();
        }
    }
}

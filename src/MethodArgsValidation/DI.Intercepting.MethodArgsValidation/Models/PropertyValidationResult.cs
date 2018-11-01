using System.Collections.Generic;

namespace DI.Intercepting.MethodArgsValidation.Core.Models
{
    /// <summary>
    /// The struct that describes property validation result
    /// </summary>
    public struct PropertyValidationResult
    {
        public PropertyValidationResult(string propertyName, IEnumerable<string> errors)
        {
            PropertyName = propertyName;
            Errors = errors;
        }

        public string PropertyName { get; }

        public IEnumerable<string> Errors { get; }
    }
}

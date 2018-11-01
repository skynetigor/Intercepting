using System;
using System.Reflection;

namespace DI.Intercepting.Core.Extensions
{
    public static class ReflectionExtension
    {
        public static bool HasAttribute(this MethodInfo method, Type attributeType, bool inherit = false)
        {
            var b = typeof(Attribute).IsAssignableFrom(attributeType);
            var c = method.GetCustomAttribute(attributeType, inherit) != null;
            return typeof(Attribute).IsAssignableFrom(attributeType) && method.GetCustomAttribute(attributeType, inherit) != null;
        }

        public static bool HasAttribute<TAttribute>(this MethodInfo method, bool inherit = false) where TAttribute : Attribute
        {
            return HasAttribute(method, typeof(TAttribute), inherit);
        }
    }
}

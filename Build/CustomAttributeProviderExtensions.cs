using System;
using System.Linq;
using System.Reflection;

namespace Build
{
    public static class CustomAttributeProviderExtensions
    {
#if NET35_OR_GREATER && !NET40_OR_GREATER
        public static T GetCustomAttribute<T>(this Module p, bool inherit = false) where T : Attribute
        {
            if (p is null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            return p.GetCustomAttribute<T, Module>(inherit);
        }

        public static T GetCustomAttribute<T>(this Assembly p, bool inherit = false) where T : Attribute
        {
            if (p is null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            return p.GetCustomAttribute<T, Assembly>(inherit);
        }

        public static T GetCustomAttribute<T>(this ParameterInfo p, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T, ParameterInfo>(inherit);
        public static T GetCustomAttribute<T>(this PropertyInfo p, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T, PropertyInfo>(inherit);
        public static T GetCustomAttribute<T>(this MethodInfo p, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T, MethodInfo>(inherit);
        public static T GetCustomAttribute<T>(this ConstructorInfo p, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T, ConstructorInfo>(inherit);
        public static T GetCustomAttribute<T>(this MemberInfo p, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T, MemberInfo>(inherit);
        public static T GetCustomAttribute<T,U>(this U p, bool inherit = false) where T : Attribute where U: ICustomAttributeProvider => (T)p.GetCustomAttributes(typeof(T), inherit).FirstOrDefault();
#endif //NET35_OR_GREATER && !NET40_OR_GREATER
        public static T GetAttribute<T>(this Module p, Type type, bool inherit = false) where T : Attribute => inherit ? throw new NotImplementedException() : p.GetCustomAttribute<T>() ?? (T)Activator.CreateInstance(typeof(T), type);
        public static T GetAttribute<T>(this Assembly p, Type type, bool inherit = false) where T : Attribute => inherit ? throw new NotImplementedException() : p.GetCustomAttribute<T>() ?? (T)Activator.CreateInstance(typeof(T), type);
        public static T GetAttribute<T>(this ParameterInfo p, Type type, bool inherit = false) where T : Attribute => inherit ? throw new NotImplementedException() : p.GetCustomAttribute<T>() ?? (T)Activator.CreateInstance(typeof(T), type);
        public static T GetAttribute<T>(this PropertyInfo p, Type type, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T>(inherit) ?? (T)Activator.CreateInstance(typeof(T), type);
        public static T GetAttribute<T>(this MethodInfo p, Type type, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T>(inherit) ?? (T)Activator.CreateInstance(typeof(T), type);
        public static T GetAttribute<T>(this ConstructorInfo p, Type type, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T>(inherit) ?? (T)Activator.CreateInstance(typeof(T), type);
        public static T GetAttribute<T>(this MemberInfo p, Type type, bool inherit = false) where T : Attribute => p.GetCustomAttribute<T>(inherit) ?? (T)Activator.CreateInstance(typeof(T), type);
        public static T GetAttribute<T>(this ICustomAttributeProvider p, Type type, bool inherit = false) where T : Attribute => (T)(p.GetCustomAttributes(typeof(T), inherit).FirstOrDefault() ?? Activator.CreateInstance(typeof(T), type));
    }
}
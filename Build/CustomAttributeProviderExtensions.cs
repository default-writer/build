using System;
using System.Linq;
using System.Reflection;

namespace Build
{
    public static class CustomAttributeProviderExtensions
    {
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
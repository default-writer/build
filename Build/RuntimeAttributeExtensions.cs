using System;
using System.Reflection;

namespace Build
{
    public static class RuntimeAttributeExtensions
    {
        /// <summary>
        /// Returns runtime attribute
        /// </summary>
        /// <typeparam name="T">Attribute, IRuntimeAttruibute type implementation</typeparam>
        /// <param name="p">Reflection member info</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this MemberInfo p, Type type) where T : RuntimeAttribute => p.GetCustomAttribute<T>() ?? (T)Activator.CreateInstance(typeof(T), type);

        /// <summary>
        /// Returns runtime attribute
        /// </summary>
        /// <typeparam name="T">Attribute, IRuntimeAttruibute type implementation</typeparam>
        /// <param name="p">Reflection member info</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this ParameterInfo p, Type type) where T : RuntimeAttribute => p.GetCustomAttribute<T>() ?? (T)Activator.CreateInstance(typeof(T), type);
    }
}
using System;
using System.Reflection;

namespace Build
{
    /// <summary>
    /// Type resolver
    /// </summary>
    /// <seealso cref="Build.ITypeResolver"/>
    public class TypeResolver : ITypeResolver
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <param name="typeFullName">The type full name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string GetTypeFullName(string typeFullName, string defaultValue) => typeFullName ?? defaultValue;
    }
}
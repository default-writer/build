using System;
using System.Reflection;

namespace Build
{
    /// <summary>
    /// Type resolver
    /// </summary>
    /// <seealso cref="Build.ITypeResolver" />
    class TypeResolver : ITypeResolver
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
        /// <param name="attribute">The attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public string GetTypeFullName(IRuntimeAttribute attribute, string defaultValue)
        {
            if (attribute != null && attribute.TypeFullName != null)
                return attribute.TypeFullName;
            return defaultValue;
        }
    }
}
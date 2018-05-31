using System;
using System.Reflection;

namespace Build
{
    /// <summary>
    /// Type resolver interface
    /// </summary>
    public interface ITypeResolver
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        Type GetType(Assembly assembly, string typeName);

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        string GetTypeFullName(IRuntimeAttribute attribute, string defaultValue);
    }
}
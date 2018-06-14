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
    }
}
using System;
using System.Reflection;

namespace Build
{
    /// <summary>
    /// Type resolver
    /// </summary>
    /// <seealso cref="Build.ITypeResolver"/>
    public sealed class TypeResolver : ITypeResolver
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);
    }
}
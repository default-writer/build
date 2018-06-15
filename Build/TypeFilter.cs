using System;

namespace Build
{
    /// <summary>
    /// Type filter
    /// </summary>
    /// <seealso cref="Build.ITypeFilter"/>
    public sealed class TypeFilter : ITypeFilter
    {
        /// <summary>
        /// Determines whether this instance can create the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can create the specified type; otherwise, <c>false</c>.</returns>
        public bool CanCreate(Type type) => CanRegister(type);

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRegister(Type type) => type != null && type.IsClass && !type.IsAbstract && !IsSpecialType(type);

        /// <summary>
        /// Determines whether [is special type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [is special type] [the specified type]; otherwise, <c>false</c>.</returns>
        static bool IsSpecialType(Type type) => typeof(Type).IsAssignableFrom(type) || typeof(Attribute).IsAssignableFrom(type) || typeof(MarshalByRefObject).IsAssignableFrom(type);
    }
}
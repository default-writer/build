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
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRegister(Type type) => type != null && !type.IsAbstract && !IsSpecialType(type);

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRegisterParameter(Type type) => type != null && type.IsClass && !type.IsAbstract && !IsSpecialType(type);

        /// <summary>
        /// Checks type compatibility
        /// </summary>
        /// <param name="parameterType"></param>
        /// <param name="attributeType"></param>
        /// <returns>True if parameter matches the criteria</returns>
        public bool CheckTypeFullName(Type parameterType, Type attributeType) => parameterType.IsAssignableFrom(attributeType);

        /// <summary>
        /// Determines whether [is special type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [is special type] [the specified type]; otherwise, <c>false</c>.</returns>
        static bool IsSpecialType(Type type) => typeof(IntPtr).IsAssignableFrom(type) || typeof(Type).IsAssignableFrom(type) || typeof(Attribute).IsAssignableFrom(type) || typeof(MarshalByRefObject).IsAssignableFrom(type);
    }
}
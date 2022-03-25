using System;
using System.Linq;
using Build;

namespace Classes
{
    public sealed class InterfaceTypeFilter : ITypeFilter
    {
        /// <summary>
        /// Determines whether this instance can create the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can create the specified type; otherwise, <c>false</c>.</returns>
        #pragma warning disable CA1822
        public bool CanCreate(Type type, bool useValueTypes = false) => InterfaceTypeFilterExtensions.CanCreate(type, useValueTypes);
        #pragma warning restore

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRegister(Type type, bool useValueTypes = false) => InterfaceTypeFilterExtensions.CanRegister(type, useValueTypes);

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRegisterParameter(Type type, bool useValueTypes = false) => InterfaceTypeFilterExtensions.CanRegisterParameter(type, useValueTypes);

        /// <summary>
        /// Checks type compatibility
        /// </summary>
        /// <param name="parameterType"></param>
        /// <param name="attributeType"></param>
        /// <returns>True if parameter matches the criteria</returns>
        public bool CheckTypeFullName(Type parameterType, Type attributeType) => InterfaceTypeFilterExtensions.CheckTypeFullName(parameterType, attributeType);
    }

    internal static class InterfaceTypeFilterExtensions
    {
        /// <summary>
        /// Determines whether this instance can create the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can create the specified type; otherwise, <c>false</c>.</returns>
        public static bool CanCreate(Type type, bool useValueTypes = false) => type != null && type.IsClass && !type.IsAbstract && !IsSpecialType(type, useValueTypes);

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanRegister(Type type, bool useValueTypes = false) => type != null && type.IsInterface && type.GetCustomAttributes(typeof(InterfaceAttribute), false).Length != 0;

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanRegisterParameter(Type type, bool useValueTypes = false) => type != null  && ((useValueTypes && (type.IsValueType || type.IsEnum)) || (!useValueTypes && type.IsInterface && type.GetCustomAttributes(typeof(InterfaceAttribute), false).Length != 0));

        /// <summary>
        /// Checks type compatibility
        /// </summary>
        /// <param name="parameterType"></param>
        /// <param name="attributeType"></param>
        /// <returns>True if parameter matches the criteria</returns>
        public static bool CheckTypeFullName(Type parameterType, Type attributeType) => parameterType.IsAssignableFrom(attributeType);

        /// <summary>
        /// Determines whether [is special type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [is special type] [the specified type]; otherwise, <c>false</c>.</returns>
        static bool IsSpecialType(Type type, bool useValueTypes = false) => typeof(IntPtr).IsAssignableFrom(type)
            || typeof(Type).IsAssignableFrom(type)
            || typeof(Attribute).IsAssignableFrom(type)
            || typeof(MarshalByRefObject).IsAssignableFrom(type)
            || typeof(Exception).IsAssignableFrom(type)
            || (typeof(Func<>).Name == type.Name)
            || (!useValueTypes && typeof(object).Assembly.ExportedTypes.Contains(type));
    }
}
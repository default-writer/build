using System;

namespace Build
{
    /// <summary>
    /// Type filter interface
    /// </summary>
    public interface ITypeFilter
    {
        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="useValueTypes">Allows use of value types.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        bool CanRegister(Type type, bool useValueTypes = false);

        /// <summary>
        /// Determines whether this instance can register the specified parameter type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="useValueTypes">Allows use of value types.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified parameter type; otherwise, <c>false</c>.
        /// </returns>
        bool CanRegisterParameter(Type type, bool useValueTypes = false);

        /// <summary>
        /// Checks type compatibility
        /// </summary>
        /// <param name="parameterType"></param>
        /// <param name="attributeType"></param>
        /// <returns>True if parameter matches the criteria</returns>
        bool CheckTypeFullName(Type parameterType, Type attributeType);
    }
}
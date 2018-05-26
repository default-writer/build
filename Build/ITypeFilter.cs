using System;

namespace Build
{
    /// <summary>
    /// Type filter interface
    /// </summary>
    public interface ITypeFilter
    {
        /// <summary>
        /// Determines whether this instance can create the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can create the specified type; otherwise, <c>false</c>.</returns>
        bool CanCreate(Type type);

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        bool CanRegister(Type type);
    }
}

using System.Collections.Generic;

namespace Build
{
    /// <summary>
    /// Type parser interface
    /// </summary>
    public interface ITypeParser
    {
        /// <summary>
        /// Finds the specified type.
        /// </summary>
        /// <param name="id">The type.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        IRuntimeType Find(string id, string[] args, IEnumerable<IRuntimeType> types);
    }
}

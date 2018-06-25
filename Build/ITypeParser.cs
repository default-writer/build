using System.Collections.Generic;

namespace Build
{
    /// <summary>
    /// Type parser interface
    /// </summary>
    public interface ITypeParser
    {
        /// <summary>
        /// Finds all matches for the specified type.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns>Returns runtime type instances</returns>
        IEnumerable<IRuntimeType> FindAll(string id, IEnumerable<string> args, IEnumerable<IRuntimeType> types);
    }
}
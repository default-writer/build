using System.Collections.Generic;

namespace Build
{
    /// <summary>
    /// Type parser interface
    /// </summary>
    public interface ITypeParser
    {
        /// <summary>
        /// Clears all precomputed registered type invariants
        /// </summary>
        void Clear();

        /// <summary>
        /// Finds all matches for the specified type.
        /// </summary>
        /// <param name="typeId">The id.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns>Returns runtime type instances</returns>
        IEnumerable<IRuntimeType> FindRuntimeTypes(string typeId, IEnumerable<string> args, IEnumerable<IRuntimeType> types);
    }
}
using System.Collections.Generic;

namespace Build
{
    /// <summary>
    /// Runtime type interface
    /// </summary>
    public interface IRuntimeType
    {
        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <value>The attribute.</value>
        IRuntimeAttribute Attribute { get; }

        /// <summary>
        /// Gets the full name of hosted runtime type
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string Id { get; }

        /// <summary>
        /// Gets the parameters count.
        /// </summary>
        /// <value>The parameters count.</value>
        int ParametersCount { get; }

        /// <summary>
        /// Gets the runtime parameters.
        /// </summary>
        /// <value>The runtime parameters.</value>
        IEnumerable<IRuntimeType> RuntimeParameters { get; }

        /// <summary>
        /// Determines whether [is assignable from] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// <c>true</c> if [is assignable from] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool IsAssignableFrom(string id);
    }
}

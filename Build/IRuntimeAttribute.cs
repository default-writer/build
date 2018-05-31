using System;

namespace Build
{
    public interface IRuntimeAttribute
    {
#if PARENT_STRATEGY

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        Guid Guid { get; }

#endif

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        RuntimeInstance RuntimeInstance { get; }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <value>The full name of the type.</value>
        string TypeFullName { get; }

        /// <summary>
        /// Gets the type of the runtime.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        IRuntimeAttribute GetRuntimeType(string id);

        /// <summary>
        /// Registers the type of the runtime.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="attribute">The attribute.</param>
        void RegisterRuntimeType(string id, IRuntimeAttribute attribute);
    }
}
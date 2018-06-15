using System;
using System.Collections.Generic;

namespace Build
{
    /// <summary>
    /// Abstract class for runtime type attribute
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    public abstract class RuntimeAttribute : Attribute, IRuntimeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeAttribute"/> class.
        /// </summary>
        protected RuntimeAttribute(string typeFullName = default) => TypeFullName = typeFullName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        protected RuntimeAttribute(Type type)
        {
            Type = type;
            TypeFullName = type.FullName;
        }

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public abstract RuntimeInstance RuntimeInstance { get; }

        /// <summary>
        /// Gets the runtime types.
        /// </summary>
        /// <value>The runtime types.</value>
        public IDictionary<string, IRuntimeAttribute> RuntimeTypes { get; } = new Dictionary<string, IRuntimeAttribute>();

        /// <summary>
        /// Type
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <value>The full name of the type.</value>
        public string TypeFullName { get; }

        /// <summary>
        /// Gets the type of the runtime.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IRuntimeAttribute GetRuntimeType(string id)
        {
            if (!RuntimeTypes.ContainsKey(id))
                return this;
            return RuntimeTypes[id];
        }

        /// <summary>
        /// Registers the type of the runtime.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="attribute">The runtime attribute.</param>
        public void RegisterRuntimeType(string id, IRuntimeAttribute attribute) => RuntimeTypes[id] = attribute;
    }
}
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
        protected RuntimeAttribute(Type type) => TypeFullName = type.ToString();

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public abstract RuntimeInstance RuntimeInstance { get; }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <value>The full name of the type.</value>
        public string TypeFullName { get; }

        /// <summary>
        /// Gets the runtime type attributes.
        /// </summary>
        /// <value>The runtime types.</value>
        IDictionary<string, IRuntimeAttribute> IdAttributeDictionary { get; } = new Dictionary<string, IRuntimeAttribute>();

        /// <summary>
        /// Gets the runtime type attribute.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IRuntimeAttribute GetAttribute(string id)
        {
            if (!IdAttributeDictionary.ContainsKey(id))
                return this;
            return IdAttributeDictionary[id];
        }

        /// <summary>
        /// Registers the type of the runtime.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="attribute">The runtime attribute.</param>
        /// <param name="defaultTypeAttributeOverwrite">
        /// Parameter defaults to true for automatic type attribute overwrite. If value is false
        /// exception will be thrown for type attribute overwrites
        /// </param>
        public void RegisterRuntimeType(string id, IRuntimeAttribute attribute, bool defaultTypeAttributeOverwrite)
        {
            if (!defaultTypeAttributeOverwrite && IdAttributeDictionary.ContainsKey(id))
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", id));
            IdAttributeDictionary[id] = attribute;
        }
    }
}
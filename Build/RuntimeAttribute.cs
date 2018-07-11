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
        readonly string _id;

        readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        protected RuntimeAttribute(Type type) => _type = type;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeAttribute"/> class.
        /// </summary>
        protected RuntimeAttribute(string typeFullName = default) => _id = typeFullName;

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public abstract RuntimeInstance RuntimeInstance { get; }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <value>The full name of the type.</value>
        public string TypeFullName => _type == null ? _id : _type.ToString();

        /// <summary>
        /// Gets the runtime type attributes.
        /// </summary>
        /// <value>The runtime types.</value>
        IDictionary<string, IRuntimeAttribute> References { get; } = new Dictionary<string, IRuntimeAttribute>();

        /// <summary>
        /// Gets referenced runtime attribute.
        /// </summary>
        /// <param name="typeId">The identifier.</param>
        /// <returns></returns>
        public IRuntimeAttribute GetReferenceAttribute(string typeId)
        {
            if (!References.ContainsKey(typeId))
                return this;
            return References[typeId];
        }

        /// <summary>
        /// Registers referenced runtime attribute.
        /// </summary>
        /// <param name="typeId">The identifier.</param>
        /// <param name="attribute">The runtime attribute.</param>
        /// <param name="defaultTypeAttributeOverwrite">
        /// Parameter defaults to true for automatic type attribute overwrite. If value is false
        /// exception will be thrown for type attribute overwrites
        /// </param>
        public void RegisterReferenceAttrubute(string typeId, IRuntimeAttribute attribute, bool defaultTypeAttributeOverwrite)
        {
            if (!defaultTypeAttributeOverwrite && References.ContainsKey(typeId))
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", typeId));
            References[typeId] = attribute;
        }
    }
}
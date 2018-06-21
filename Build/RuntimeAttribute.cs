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
        /// Gets the runtime types.
        /// </summary>
        /// <value>The runtime types.</value>
        IDictionary<string, IRuntimeAttribute> RuntimeTypes { get; } = new Dictionary<string, IRuntimeAttribute>();

        /// <summary>
        /// Gets default value for type
        /// </summary>
        public object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                if (type.IsEnum)
                {
                    var enums = Enum.GetValues(type);
                    if (enums.Length > 0)
                        return enums.GetValue(0);
                }
                return Activator.CreateInstance(type);
            }
            return default;
        }

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
        /// <param name="defaultTypeAttributeOverwrite">
        /// Parameter defaults to true for automatic type attribute overwrite. If value is false
        /// exception will be thrown for type attribute overwrites
        /// </param>
        public void RegisterRuntimeType(string id, IRuntimeAttribute attribute, bool defaultTypeAttributeOverwrite)
        {
            if (!defaultTypeAttributeOverwrite && RuntimeTypes.ContainsKey(id))
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", id));
            RuntimeTypes[id] = attribute;
        }
    }
}
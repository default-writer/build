using System.Collections.Generic;
using System.Reflection;

namespace Build
{
    static class PropertyCache
    {
        /// <summary>
        /// The raw values
        /// </summary>
        static readonly IDictionary<string, PropertyInfo> _objects = new Dictionary<string, PropertyInfo>();

        /// <summary>
        /// Gets the property from the runtime type.
        /// </summary>
        /// <param name="typeId">Id</param>
        /// <value>Value</value>
        public static PropertyInfo GetPropertyInfo(string typeId)
        {
            if (!_objects.ContainsKey(typeId))
                return default;
            return _objects[typeId];
        }

        /// <summary>
        /// Sets the property to the runtime type.
        /// </summary>
        /// <param name="typeId">Id</param>
        /// <param name="value">Value</param>
        public static void SetPropertyInfo(string typeId, PropertyInfo value) => _objects[typeId] = value;
    }
}
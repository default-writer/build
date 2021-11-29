using System.Collections.Generic;
using System.Reflection;

namespace Build
{
    internal static class PropertyCache
    {
        /// <summary>
        /// The raw values
        /// </summary>
        private static readonly IDictionary<string, PropertyInfo> _objects = new Dictionary<string, PropertyInfo>();

        /// <summary>
        /// Gets the property from the runtime type.
        /// </summary>
        /// <param name="runtimeType">Id</param>
        /// <value>Value</value>
        public static PropertyInfo GetPropertyInfo(IRuntimeType runtimeType) => _objects[Format.GetActivatorType(runtimeType)];

        /// <summary>
        /// Sets the property to the runtime type.
        /// </summary>
        /// <param name="runtimeType">Id</param>
        /// <param name="value">Value</param>
        public static void SetPropertyInfo(IRuntimeType runtimeType, PropertyInfo value) => _objects[Format.GetActivatorType(runtimeType)] = value;
    }
}
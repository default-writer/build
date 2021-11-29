using System;
using System.Linq;
using Build;

namespace Classes
{
    class PropertyTypeActivator : ITypeActivator
    {
        /// <summary>
        /// Creates the instance with type inferenced evaluated arguments.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public object CreateInstance(IRuntimeType instance, IRuntimeType type, IRuntimeAttribute attribute)
        {
            var obj = instance.GetValue(type.Attribute, type.Id) ?? Activator.CreateInstance(instance.ActivatorType);
            var runtimeTypes = instance.RuntimeTypes.ToArray();
            for (int i = 0; i < runtimeTypes.Length; i++)
            {
                var runtimeType = runtimeTypes[i];
                var value = runtimeType.Evaluate(type, attribute, i);
                var propertyInfo = PropertyCache.GetPropertyInfo(runtimeType);
                propertyInfo.SetValue(obj, value);
            }
            return obj;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        public object CreateInstance(IRuntimeType instance)
        {
            var obj = Activator.CreateInstance(instance.ActivatorType);
            var runtimeTypes = instance.RuntimeTypes.ToArray();
            for (int i = 0; i < runtimeTypes.Length; i++)
            {
                var runtimeType = runtimeTypes[i];
                var value = runtimeType.GetValue(instance.Attribute, instance.Id);
                var propertyInfo = PropertyCache.GetPropertyInfo(runtimeType);
                propertyInfo.SetValue(obj, value);
            }
            return obj;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        public object CreateValueInstance(IRuntimeType instance) => Activator.CreateInstance(instance.ActivatorType);
    }
}
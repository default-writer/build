using System;
using System.Linq;
using System.Reflection;

namespace Build.Tests.Classes
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
                var p = runtimeTypes[i];
                var value = p.Evaluate(type, attribute, i);
                var id = Format.GetActivatorType(p);
                var propertyInfo = (PropertyInfo)instance.GetValue(id);
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
                var p = runtimeTypes[i];
                var value = p.GetValue(instance.Attribute, instance.Id);
                var id = Format.GetActivatorType(p);
                var propertyInfo = (PropertyInfo)instance.GetValue(id);
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
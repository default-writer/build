using System;

namespace Build
{
    public sealed class TypeActivator : ITypeActivator
    {
        /// <summary>
        /// Creates the instance with type inferenced evaluated arguments.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public object CreateInstance(IRuntimeType instance, IRuntimeType type, IRuntimeAttribute attribute) => Activator.CreateInstance(instance.ActivatorType, instance.RuntimeTypes.Evaluate(type, attribute));

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        public object CreateInstance(IRuntimeType instance) => Activator.CreateInstance(instance.ActivatorType, instance.RuntimeTypes.Values(instance.Attribute, instance.Id));

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        public object CreateValueInstance(IRuntimeType instance) => Activator.CreateInstance(instance.ActivatorType);
    }
}
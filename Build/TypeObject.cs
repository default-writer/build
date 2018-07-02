using System;

namespace Build
{
    public class TypeObject : ITypeObject
    {
        public TypeObject(ITypeActivator runtimeTypeActivator, IRuntimeAttribute runtimeAttribute, Type runtimeType, bool defaultTypeInstantiation)
        {
            RuntimeAttribute = runtimeAttribute;
            RuntimeType = new RuntimeType(runtimeTypeActivator, runtimeAttribute, runtimeType, defaultTypeInstantiation);
        }

        public IRuntimeAttribute RuntimeAttribute { get; }
        public IRuntimeType RuntimeType { get; set; }

        /// <summary>
        /// Gets the type full name
        /// </summary>
        public string TypeFullName => RuntimeAttribute.TypeFullName ?? RuntimeType.TypeFullName;

#if EXPERIMENTAL
        /// <summary>
        /// Initializes runtime type
        /// </summary>
        /// <param name="runtimeType"></param>
        public void SetRuntimeType(IRuntimeType runtimeType) => RuntimeType = runtimeType;
#endif
    }
}
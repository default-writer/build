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
        public IRuntimeType RuntimeType { get; }

        /// <summary>
        /// Gets the type full name
        /// </summary>
        public string TypeFullName => RuntimeAttribute.TypeFullName ?? RuntimeType.TypeFullName;
    }
}
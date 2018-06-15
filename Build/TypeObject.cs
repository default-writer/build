using System;
using System.Collections.Generic;

namespace Build
{
    class TypeObject : ITypeObject
    {
        public TypeObject(IRuntimeAttribute runtimeAttribute, Type runtimeType, bool defaultTypeInstantiation)
        {
            RuntimeAttribute = runtimeAttribute;
            RuntimeType = new RuntimeType(runtimeAttribute, runtimeType, defaultTypeInstantiation);
        }

        public IRuntimeAttribute RuntimeAttribute { get; }
        public IRuntimeType RuntimeType { get; }

        /// <summary>
        /// Gets the type full name
        /// </summary>
        public string TypeFullName => RuntimeAttribute.TypeFullName ?? RuntimeType.TypeFullName;
    }
}
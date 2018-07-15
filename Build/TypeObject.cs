using System;
using System.Collections.Generic;

namespace Build
{
    public class TypeObject : ITypeObject
    {
        public TypeObject(ITypeActivator runtimeTypeActivator, IRuntimeAttribute runtimeAttribute, Type runtimeType, IEnumerable<string> typeParameters, bool defaultTypeInstantiation)
        {
            RuntimeAttribute = runtimeAttribute;
            TypeParameters = typeParameters;
            RuntimeType = new RuntimeType(runtimeTypeActivator, runtimeAttribute, runtimeType, defaultTypeInstantiation);
        }

        /// <summary>
        /// Runtime attribute
        /// </summary>
        public IRuntimeAttribute RuntimeAttribute { get; }

        /// <summary>
        /// Runtime type
        /// </summary>
        public IRuntimeType RuntimeType { get; private set; }

        /// <summary>
        /// Gets the object full name
        /// </summary>
        /// <remarks>If runtime attruibute type full name is unknown, then runtime type full name will be used</remarks>
        public string Type => RuntimeAttribute.TypeFullName ?? RuntimeType.TypeFullName;

        /// <summary>
        /// Type full name with parameters
        /// </summary>
        public string TypeIdentity => Format.GetConstructor(Type, TypeParameters);

        /// <summary>
        /// Type parameters full name
        /// </summary>
        public IEnumerable<string> TypeParameters { get; }

        /// <summary>
        /// Sets
        /// </summary>
        /// <param name="runtimeType"></param>
        public void SetRuntimeType(IRuntimeType runtimeType) => RuntimeType = runtimeType;
    }
}
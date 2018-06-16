using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build
{
    public sealed class TypeDependencyObject : TypeObject, ITypeDependencyObject
    {
        public TypeDependencyObject(IDependencyAttribute runtimeAttribute, IEnumerable<ITypeInjectionObject> injectionObjects, Type runtimeType, bool defaultTypeInstantiation) : base(GetDependencyAttribute(runtimeAttribute, runtimeType), runtimeType, defaultTypeInstantiation)
        {
            var dependencyAttribute = (IDependencyAttribute)RuntimeAttribute;
            DependencyAttribute = dependencyAttribute;
            InjectionObjects = injectionObjects;
            TypeParameters = InjectionObjects.Select(p => p.RuntimeType.TypeFullName);
            TypeFullNameWithParameters = Format.GetConstructorWithParameters(TypeFullName, TypeParameters);
        }

        /// <summary>
        /// Dependency attribute
        /// </summary>
        public IDependencyAttribute DependencyAttribute { get; }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        public IEnumerable<ITypeInjectionObject> InjectionObjects { get; }

        /// <summary>
        /// Type full name with parameters
        /// </summary>
        public string TypeFullNameWithParameters { get; }

        /// <summary>
        /// Type parameters full name
        /// </summary>
        public IEnumerable<string> TypeParameters { get; }

        /// <summary>
        /// Gets the dependency attribute.(ConstructorInfo's DeclaringType)
        /// </summary>
        /// <param name="runtimeAttribute">The runtime attribute.</param>
        /// <param name="runtimeType">Type to be instantiated</param>
        /// <returns>Returns custom dependency attrubute</returns>
        static IRuntimeAttribute GetDependencyAttribute(IRuntimeAttribute runtimeAttribute, Type runtimeType) => runtimeAttribute ?? new DependencyAttribute(runtimeType, RuntimeInstance.CreateInstance);
    }
}
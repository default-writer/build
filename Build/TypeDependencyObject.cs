using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build
{
    public sealed class TypeDependencyObject : TypeObject, ITypeDependencyObject
    {
        public TypeDependencyObject(ConstructorInfo constructorInfo, Type runtimeType, bool defaultTypeInstantiation) : base(GetDependencyAttribute(constructorInfo, runtimeType), runtimeType, defaultTypeInstantiation)
        {
            var dependencyAttribute = (DependencyAttribute)RuntimeAttribute;
            DependencyAttribute = dependencyAttribute;
            InjectionObjects = new List<ITypeInjectionObject>(constructorInfo.GetParameters().Select(p => new TypeInjectionObject(p, defaultTypeInstantiation)));
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
        /// <param name="constructorInfo">The constructor.</param>
        /// <param name="runtimeType">Type to be instantiated</param>
        /// <returns>Returns custom dependency attrubute</returns>
        static DependencyAttribute GetDependencyAttribute(ConstructorInfo constructorInfo, Type runtimeType) => constructorInfo.GetCustomAttribute<DependencyAttribute>() ?? new DependencyAttribute(runtimeType, RuntimeInstance.CreateInstance);
    }
}
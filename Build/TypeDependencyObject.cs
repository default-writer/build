using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build
{
    class TypeDependencyObject : TypeObject, ITypeDependencyObject
    {
        public TypeDependencyObject(ConstructorInfo constructorInfo, bool defaultTypeInstantiation) : base(GetDependencyAttribute(constructorInfo), constructorInfo.DeclaringType, defaultTypeInstantiation)
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
        /// <returns></returns>
        static DependencyAttribute GetDependencyAttribute(ConstructorInfo constructorInfo) => constructorInfo.GetCustomAttribute<DependencyAttribute>() ?? new DependencyAttribute(constructorInfo.DeclaringType, RuntimeInstance.CreateInstance);
    }
}
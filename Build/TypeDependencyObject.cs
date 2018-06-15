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
            InjectedTypes = InjectionObjects.Select(p => p.RuntimeType.TypeFullName);
        }

        public IDependencyAttribute DependencyAttribute { get; }

        public IEnumerable<string> InjectedTypes { get; }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        public IEnumerable<ITypeInjectionObject> InjectionObjects { get; }

        /// <summary>
        /// Gets the dependency attribute.(ConstructorInfo's DeclaringType)
        /// </summary>
        /// <param name="constructorInfo">The constructor.</param>
        /// <returns></returns>
        static DependencyAttribute GetDependencyAttribute(ConstructorInfo constructorInfo) => constructorInfo.GetCustomAttribute<DependencyAttribute>() ?? new DependencyAttribute(constructorInfo.DeclaringType, RuntimeInstance.CreateInstance);
    }
}
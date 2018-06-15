using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build
{
    class TypeDependencyObject : ITypeDependencyObject
    {
        public TypeDependencyObject(ConstructorInfo constructorInfo, bool defaultTypeInstantiation)
        {
            DependencyAttribute = GetDependencyAttribute(constructorInfo);
            RuntimeType = new RuntimeType(DependencyAttribute, constructorInfo.DeclaringType, defaultTypeInstantiation);
            InjectionObjects = new List<ITypeInjectionObject>(constructorInfo.GetParameters().Select(p => new TypeInjectionObject(p, defaultTypeInstantiation)));
            InjectionObjectsFullNames = InjectionObjects.Select(p => p.RuntimeType.FullName);
        }

        public IDependencyAttribute DependencyAttribute { get; }
        public IEnumerable<ITypeInjectionObject> InjectionObjects { get; }
        public IEnumerable<string> InjectionObjectsFullNames { get; }
        public IRuntimeType RuntimeType { get; }

        /// <summary>
        /// Gets the dependency attribute.(ConstructorInfo's DeclaringType)
        /// </summary>
        /// <param name="constructorInfo">The constructor.</param>
        /// <returns></returns>
        static DependencyAttribute GetDependencyAttribute(ConstructorInfo constructorInfo) => constructorInfo.GetCustomAttribute<DependencyAttribute>() ?? new DependencyAttribute(constructorInfo.DeclaringType, RuntimeInstance.CreateInstance);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    public sealed class TypeDependencyObject : TypeObject, ITypeDependencyObject
    {
        public TypeDependencyObject(ITypeActivator runtimeTypeActivator, IDependencyAttribute dependencyAttribute, IEnumerable<ITypeInjectionObject> injectionObjects, Type runtimeType, bool defaultTypeInstantiation) : base(runtimeTypeActivator, dependencyAttribute, runtimeType, injectionObjects.Select(p => p.RuntimeType.TypeFullName), defaultTypeInstantiation)
        {
            DependencyAttribute = dependencyAttribute;
            InjectionObjects = injectionObjects;
        }

        /// <summary>
        /// Dependency attribute
        /// </summary>
        public IDependencyAttribute DependencyAttribute { get; }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        public IEnumerable<ITypeInjectionObject> InjectionObjects { get; }
    }
}
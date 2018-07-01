using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build
{
    public sealed class TypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(ITypeActivator runtimeTypeActivator, Type type, bool defaultTypeInstantiation)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            foreach (var constructorInfo in type.GetConstructors())
            {
                var runtimeAttribute = constructorInfo.GetCustomAttribute<DependencyAttribute>();
                var injectionObjects = constructorInfo.GetParameters().Select(p => new TypeInjectionObject(runtimeTypeActivator, p.GetCustomAttribute<InjectionAttribute>(), p.ParameterType, defaultTypeInstantiation));
                dependencyObjects.Add(new TypeDependencyObject(runtimeTypeActivator, runtimeAttribute, injectionObjects, constructorInfo.DeclaringType, defaultTypeInstantiation));
            }
            return dependencyObjects;
        }
    }
}
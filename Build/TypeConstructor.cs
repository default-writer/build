using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build
{
    public sealed class TypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(Type type, bool defaultTypeInstantiation)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            foreach (var constructorInfo in type.GetConstructors())
            {
                var runtimeAttribute = constructorInfo.GetCustomAttribute<DependencyAttribute>();
                var injectionObjects = constructorInfo.GetParameters().Select(p => new TypeInjectionObject(p.GetCustomAttribute<InjectionAttribute>(), p.ParameterType, defaultTypeInstantiation));
                dependencyObjects.Add(new TypeDependencyObject(runtimeAttribute, injectionObjects, constructorInfo.DeclaringType, defaultTypeInstantiation));
            }
            return dependencyObjects;
        }
    }
}
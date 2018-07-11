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
            var constructors = type.GetConstructors();
            if (constructors.Length == 0 && typeof(ValueType).IsAssignableFrom(type))
            {
                constructors = ((TypeInfo)type).DeclaredConstructors.ToArray();
                if (constructors.Length == 0)
                    constructors = ((TypeInfo)type.BaseType).DeclaredConstructors.ToArray();
            }
            foreach (var constructorInfo in constructors)
            {
                var runtimeAttribute = constructorInfo.GetAttribute<DependencyAttribute>(type);
                var injectionObjects = constructorInfo.GetParameters().Select(p => new TypeInjectionObject(runtimeTypeActivator, p.GetAttribute<InjectionAttribute>(p.ParameterType), p.ParameterType, defaultTypeInstantiation));
                dependencyObjects.Add(new TypeDependencyObject(runtimeTypeActivator, runtimeAttribute, injectionObjects, type, defaultTypeInstantiation));
            }
            return dependencyObjects;
        }
    }
}
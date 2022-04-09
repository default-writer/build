using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{

    public sealed class TypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(ITypeActivator runtimeTypeActivator, Type type, bool defaultTypeInstantiation, ITypeDependencyAttributeProvider typeDependencyAttributeProvider = null, ITypeInjectionAttributeProvider typeInjectionAttributeProvider = null)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            var constructors = type.GetConstructors();
            if (constructors.Length == 0 && typeof(ValueType).IsAssignableFrom(type))
            {
                constructors = type.GetConstructors().ToArray();
                if (constructors.Length == 0)
                    constructors = type.BaseType.GetConstructors().ToArray();
                if (constructors.Length == 0)
                {
                    var dependencyAttribute = typeDependencyAttributeProvider.GetAttribute(type, type);
                    var injectionObjects = new List<ITypeInjectionObject>(); // { new TypeInjectionObject(runtimeTypeActivator, null, null, defaultTypeInstantiation) };
                    var dependencyObject = new TypeDependencyObject(runtimeTypeActivator, dependencyAttribute, injectionObjects, type, defaultTypeInstantiation);
                    dependencyObjects.Add(dependencyObject);
                }
                return dependencyObjects;
            }
            foreach (var constructorInfo in constructors)
            {
                var dependencyAttribute = typeDependencyAttributeProvider.GetAttribute(constructorInfo, type);
                var injectionObjects = constructorInfo.GetParameters().Select(p => (ITypeInjectionObject)new TypeInjectionObject(runtimeTypeActivator, typeInjectionAttributeProvider.GetAttribute(p, p.ParameterType), p.ParameterType, defaultTypeInstantiation));
                dependencyObjects.Add(new TypeDependencyObject(runtimeTypeActivator, dependencyAttribute, injectionObjects, type, defaultTypeInstantiation));
            }
            return dependencyObjects;
        }

    }
}
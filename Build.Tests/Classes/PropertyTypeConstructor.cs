using System;
using System.Collections.Generic;
using System.Linq;

namespace Build.Tests.Classes
{
    /// <summary>
    /// The fun part: use a "magic" method to describe type dependencies
    /// </summary>
    public sealed class PropertyTypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(ITypeActivator runtimeTypeActivator, Type type, bool defaultTypeInstantiation)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            var constructors = type.GetConstructors();
            if (constructors.Length == 1)
            {
                var constructor = constructors[0];
                var constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                {
                    var injectionObjects = new List<ITypeInjectionObject>();
                    var constructorType = constructor.DeclaringType;
                    var runtimeAttribute = type.GetAttribute<PropertyDependencyAttribute>(constructorType);
                    var properties = type.GetProperties().Where((p) => !p.IsSpecialName && p.CanRead && p.CanWrite);
                    injectionObjects.AddRange(properties.Select((p) =>
                    {
                        var injectionObject = new TypeInjectionObject(runtimeTypeActivator, p.GetAttribute<PropertyInjectionAttribute>(p.PropertyType), p.PropertyType, defaultTypeInstantiation);
                        var typeId = Format.GetActivatorType(injectionObject.RuntimeType);
                        injectionObject.RuntimeType.SetValue(typeId, p);
                        return injectionObject;
                    }));
                    var dependencyObject = new TypeDependencyObject(runtimeTypeActivator, runtimeAttribute, injectionObjects, constructorType, defaultTypeInstantiation);
                    dependencyObjects.Add(dependencyObject);
                    foreach (var injectionObject in injectionObjects)
                    {
                        string typeId = Format.GetActivatorType(injectionObject.RuntimeType);
                        object value = injectionObject.RuntimeType.GetValue(typeId);
                        dependencyObject.RuntimeType.SetValue(typeId, value);
                    }
                }
            }
            return dependencyObjects;
        }
    }
}
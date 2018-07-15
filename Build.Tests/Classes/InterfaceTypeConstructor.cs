using System;
using System.Collections.Generic;
using System.Linq;

namespace Build.Tests.Classes
{
    /// <summary>
    /// The fun part: use a "magic" method to describe type dependencies
    /// </summary>
    public sealed class InterfaceTypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(ITypeActivator runtimeTypeActivator, Type type, bool defaultTypeInstantiation)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            foreach (var constructorInfo in type.GetMethods())
            {
                if (constructorInfo.Name == "Rule")
                {
                    var runtimeAttribute = constructorInfo.GetAttribute<InterfaceDependencyAttribute>(constructorInfo.ReturnType);
                    var injectionObjects = constructorInfo.GetParameters().Select(p => new TypeInjectionObject(runtimeTypeActivator, p.GetAttribute<InterfaceInjectionAttribute>(p.ParameterType), p.ParameterType, defaultTypeInstantiation));
                    dependencyObjects.Add(new TypeDependencyObject(runtimeTypeActivator, runtimeAttribute, injectionObjects, constructorInfo.ReturnType, defaultTypeInstantiation));
                }
            }
            return dependencyObjects;
        }
    }
}
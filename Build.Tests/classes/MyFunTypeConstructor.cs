using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build.Tests
{
    /// <summary>
    /// The fun part: use a "magic" method to describe type dependencies
    /// </summary>
    public sealed class MyFunTypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(Type type, bool defaultTypeInstantiation)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            foreach (var constructorInfo in type.GetMethods())
            {
                if (constructorInfo.Name == "Rule")
                {
                    var runtimeAttribute = constructorInfo.GetCustomAttribute<MyFunDependencyAttribute>();
                    var injectionObjects = constructorInfo.GetParameters().Select(p => new TypeInjectionObject(p.GetCustomAttribute<MyFunInjectionAttribute>(), p.ParameterType, defaultTypeInstantiation));
                    dependencyObjects.Add(new TypeDependencyObject(runtimeAttribute, injectionObjects, constructorInfo.ReturnType, defaultTypeInstantiation));
                }
            }
            return dependencyObjects;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Build.Interfaces
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
                    dependencyObjects.Add(new MyFunTypeDependencyObject(constructorInfo, constructorInfo.ReturnType, defaultTypeInstantiation));
            }
            return dependencyObjects;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Build.Interfaces
{
    public class MyFunTypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(Type type, bool defaultTypeInstantiation)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            foreach (var constructorInfo in type.GetConstructors())
            {
                dependencyObjects.Add(new MyFunTypeDependencyObject(constructorInfo, defaultTypeInstantiation));
            }
            return dependencyObjects;
        }
    }
}
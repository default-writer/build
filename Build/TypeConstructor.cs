using System;
using System.Collections.Generic;

namespace Build
{
    public class TypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(Type type, bool defaultTypeInstantiation)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            foreach (var constructorInfo in type.GetConstructors())
            {
                dependencyObjects.Add(new TypeDependencyObject(constructorInfo, defaultTypeInstantiation));
            }
            return dependencyObjects;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Build
{
    public interface ITypeConstructor
    {
        IEnumerable<ITypeDependencyObject> GetDependencyObjects(Type type, bool defaultTypeInstantiation);
    }
}
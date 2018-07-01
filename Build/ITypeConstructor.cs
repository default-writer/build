using System;
using System.Collections.Generic;

namespace Build
{
    /// <summary>
    /// Constructs type dependency
    /// </summary>
    public interface ITypeConstructor
    {
        IEnumerable<ITypeDependencyObject> GetDependencyObjects(ITypeActivator runtimeTypeActivator, Type type, bool defaultTypeInstantiation);
    }
}
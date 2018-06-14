using System.Collections.Generic;

namespace Build
{
    public interface ITypeDependencyObject
    {
        IDependencyAttribute DependencyAttribute { get; }
        IEnumerable<ITypeInjectionObject> InjectionObjects { get; }
        RuntimeType RuntimeType { get; }
        IEnumerable<string> InjectionObjectsFullNames { get; }
    }
}
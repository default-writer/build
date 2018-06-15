using System.Collections.Generic;

namespace Build
{
    public interface ITypeDependencyObject
    {
        IDependencyAttribute DependencyAttribute { get; }
        IEnumerable<ITypeInjectionObject> InjectionObjects { get; }
        IEnumerable<string> InjectionObjectsFullNames { get; }
        IRuntimeType RuntimeType { get; }
    }
}
using System.Collections.Generic;

namespace Build
{
    public interface ITypeDependencyObject
    {
        IDependencyAttribute DependencyAttribute { get; }
        List<ITypeInjectionObject> InjectionObjects { get; }
        RuntimeType RuntimeType { get; }
    }
}
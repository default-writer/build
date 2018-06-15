using System.Collections.Generic;

namespace Build
{
    public interface ITypeDependencyObject : ITypeObject
    {
        IDependencyAttribute DependencyAttribute { get; }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        IEnumerable<string> InjectedTypes { get; }

        IEnumerable<ITypeInjectionObject> InjectionObjects { get; }
    }
}
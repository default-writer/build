using System.Collections.Generic;

namespace Build
{
    public interface ITypeDependencyObject : ITypeObject
    {
        /// <summary>
        /// Dependency object attribute
        /// </summary>
        IDependencyAttribute DependencyAttribute { get; }

        /// <summary>
        /// List of injected objects
        /// </summary>
        IEnumerable<ITypeInjectionObject> InjectionObjects { get; }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        IEnumerable<string> TypeParameters { get; }
    }
}
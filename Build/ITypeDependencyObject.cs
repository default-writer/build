using System.Collections.Generic;

namespace Build
{
    public interface ITypeDependencyObject : ITypeObject
    {
        IDependencyAttribute DependencyAttribute { get; }

        IEnumerable<ITypeInjectionObject> InjectionObjects { get; }

        string TypeFullNameWithParameters { get; }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        IEnumerable<string> TypeParameters { get; }
    }
}
using System.Collections.Generic;

namespace Build
{
    public interface ITypeInjectionObject : ITypeObject
    {
        /// <summary>
        /// Injection object attribute
        /// </summary>
        IInjectionAttribute InjectionAttribute { get; }

        /// <summary>
        /// List of type parameters
        /// </summary>
        IEnumerable<string> TypeParameters { get; }

        /// <summary>
        /// Setup runtime type for the injected object
        /// </summary>
        /// <param name="runtimeType"></param>
        void SetRuntimeType(IRuntimeType runtimeType);
    }
}
using System.Collections.Generic;

namespace Build
{
    public interface ITypeInjectionObject : ITypeObject
    {
        IInjectionAttribute InjectionAttribute { get; }

        string TypeFullNameWithParameters { get; }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        IEnumerable<string> TypeParameters { get; }

#if EXPERIMENTAL
        /// <summary>
        /// Initializes runtime type
        /// </summary>
        /// <param name="runtimeType"></param>
        void SetRuntimeType(IRuntimeType runtimeType);
#endif
    }
}
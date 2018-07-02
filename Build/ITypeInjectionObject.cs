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
    }
}
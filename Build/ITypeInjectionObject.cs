using System.Collections.Generic;

namespace Build
{
    public interface ITypeInjectionObject : ITypeObject
    {
        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        IEnumerable<string> InjectedTypes { get; }

        IInjectionAttribute InjectionAttribute { get; }
    }
}
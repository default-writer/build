using System.Collections.Generic;

namespace Build
{
    public interface IInjectionAttribute : IRuntimeAttribute
    {
        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> Arguments { get; }

        /// <summary>
        /// Checks that selected index is within parameters array bounds
        /// </summary>
        /// <param name="index">Value index in parameters array</param>
        /// <returns>Returns true if selected index is within parameters array bounds</returns>
        bool CheckBounds(int index);

        /// <summary>
        /// Gets injected object parameters
        /// </summary>
        /// <param name="index">Value index in parameters array</param>
        /// <returns>Returns value in array at specified index</returns>
        object GetObject(int index);
    }
}
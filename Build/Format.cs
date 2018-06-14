using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    /// <summary>
    /// Format patterns for class constructors and constructor parameters
    /// </summary>
    static class Format
    {
        /// <summary>
        /// Gets the full name of the constructor.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string GetConstructorFullName(string constructor, IEnumerable<string> parameters) => string.Format("{0}({1})", constructor, string.Join(",", parameters));

        /// <summary>
        /// Gets the full name of the constructor parameter.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public static string GetConstructorParameterFullName(string typeFullName, int? i) => string.Format("{0}:({1})", typeFullName, i);

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetParametersFullName(object[] args) => args == null ? Array.Empty<string>() : args.Select(p => (p ?? typeof(object)).GetType().FullName).ToArray();
    }
}
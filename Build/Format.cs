using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    /// <summary>
    /// Format patterns for class constructors and constructor parameters
    /// </summary>
    public static class Format
    {
        /// <summary>
        /// Gets the full name of the constructor.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string GetConstructorWithParameters(string constructor, IEnumerable<string> parameters) => string.Format("{0}({1})", constructor, string.Join(",", parameters));

        /// <summary>
        /// Gets object full name
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetParameterFullName(object o) => (o ?? typeof(object)).GetType().ToString();

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetParametersFullName(object[] args) => args == null ? new string[0] : args.Select(GetParameterFullName);

        /// <summary>
        /// Converts runtime type to corresponding parameter string value
        /// </summary>
        /// <param name="runtimeType">Runtime type</param>
        /// <returns>Returns runtime type parameter</returns>
        public static string GetRuntimeTypeParameter(IRuntimeType runtimeType) => string.Format(":{0}", runtimeType.Type);
    }
}
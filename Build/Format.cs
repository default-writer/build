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
        /// Converts runtime type to corresponding parameter string value
        /// </summary>
        /// <param name="runtimeType">Runtime type</param>
        /// <returns>Returns runtime type parameter</returns>
        public static string GetActivatorType(IRuntimeType runtimeType) => string.Format(":{0}", runtimeType.ActivatorType);

        /// <summary>
        /// Gets the full name of the constructor.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string GetConstructor(string constructor, IEnumerable<string> parameters) => string.Format("{0}({1})", constructor, string.Join(",", parameters));

        /// <summary>
        /// Gets object full name
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetName(object o) => o?.GetType().ToString();

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetNames(object[] args) => args == null || args.Length == 0 ? Array.Empty<string>() : args.Select(GetName);

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetNames(string[] args) => args == null || args.Length == 0 ? Array.Empty<string>() : args;

        /// <summary>
        /// Gets object full name
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Type GetType(object o) => o?.GetType();

        /// <summary>
        /// Gets the type of the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes(object[] args) => args == null || args.Length == 0 ? Array.Empty<Type>() : args.Select(GetType);
    }
}
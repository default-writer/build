using System;
using System.Collections.Generic;
using System.Reflection;

namespace Build
{
    /// <summary>
    /// Public interface for type container
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Aliased types.
        /// </summary>
        IEnumerable<string> RuntimeAliasedTypes { get; }

        /// <summary>
        /// Non-aliased types.
        /// </summary>
        IEnumerable<string> RuntimeNonAliasedTypes { get; }

        /// <summary>
        /// Type aliases
        /// </summary>
        IEnumerable<string> RuntimeTypeAliases { get; }

        /// <summary>
        /// Runtime types.
        /// </summary>
        IEnumerable<string> RuntimeTypes { get; }

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        T CreateInstance<T>(params object[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(Type type, params object[] args);

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="id">Type identifier with/without parameters 'id(args)' or 'id'</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(string id, params object[] args);

        /// <summary>
        /// Registers all supported types in assembly
        /// </summary>
        /// <param name="assembly">Assembly for add type identifiers</param>
        /// <param name="exclusionTypes">List of assembly types to ignore</param>
        void RegisterAssembly(Assembly assembly, string[] exclusionTypes);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        void RegisterType(Type type);

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        void RegisterType<T>();

        /// <summary>
        /// Resets information about type registration
        /// </summary>
        void Reset();
    }
}

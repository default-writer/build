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
        /// Type builder
        /// </summary>
        ITypeBuilder Builder { get; }

        /// <summary>
        /// True if container is locked
        /// </summary>
        bool IsLocked { get; }

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
        /// Type builder reference
        /// </summary>
        TypeBuilder TypeBuilder { get; }

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <returns>Returns instance of identified type</returns>
        T CreateInstance<T>(params object[] args);

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <returns>Returns instance of identified type</returns>
        T CreateInstance<T>();

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(Type type, params object[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(Type type, params string[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(Type type, params Type[] args);

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="typeFullName">Type identifier with/without parameters 'id(args)' or 'id'</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(string typeFullName, params object[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(string typeId, params string[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(string typeId, params Type[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(Type type);

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        T CreateInstance<T>(params string[] args);

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        T CreateInstance<T>(params Type[] args);

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="typeFullName">Type identifier with/without parameters 'id(args)' or 'id'</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(string typeFullName);

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        T GetInstance<T>(params object[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeFullName">Type identifier</param>
        /// <returns>Returns instance of identified type</returns>
        object GetInstance(string typeFullName);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeFullName">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object GetInstance(string typeFullName, params string[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeFullName">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object GetInstance(string typeFullName, params object[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeFullName">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object GetInstance(string typeFullName, params Type[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object GetInstance(Type type, params Type[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <returns>Returns instance of identified type</returns>
        object GetInstance(Type type);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object GetInstance(Type type, params string[] args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object GetInstance(Type type, params object[] args);

        /// <summary>
        /// Locks the container. Pre-computes all registered type invariants for lookup table speed up
        /// </summary>
        void Lock();

        /// <summary>
        /// Registers all supported types in assembly
        /// </summary>
        /// <param name="assembly">Assembly for add type identifiers</param>
        /// <param name="exclusionTypes">List of assembly types to ignore</param>
        void RegisterAssembly(Assembly assembly, IEnumerable<string> exclusionTypes);

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Constructor arguments</param>
        void RegisterType<T>(params object[] args);

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Constructor arguments</param>
        void RegisterType<T>(params string[] args);

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Constructor arguments</param>
        void RegisterType<T>(params Type[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        void RegisterType(Type type, params object[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        void RegisterType(Type type);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        void RegisterType(string typeId);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        void RegisterType(string typeId, params object[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        void RegisterType(string typeId, params string[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        void RegisterType(string typeId, params Type[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        void RegisterType(Type type, params string[] args);

        /// <summary>
        /// Regeters type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="args">The args.</param>
        void RegisterType(Type type, params Type[] args);

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        void RegisterType<T>();

        /// <summary>
        /// Resets information about type registration. Also, resets freezed containers
        /// </summary>
        void Reset();

        /// <summary>
        /// Unlocks the container. Flushes all pre-computed registered type invariants for lookup table speed up
        /// </summary>
        void Unlock();
    }
}
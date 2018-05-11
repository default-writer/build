using System;
using System.Reflection;

namespace Build
{
    /// <summary>
    /// Public interface for type container
    /// </summary>
    public interface IContainer
    {
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
        /// <param name="type">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(Type type, params object[] args);

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="typeId">Type identifier with/without parameters 'typeid(args)' or 'typeid'</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(string typeId, params object[] args);

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        void RegisterType<T>();

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        void Register(Type type);

        /// <summary>
        /// Registers all supported types in assembly
        /// </summary>
        /// <param name="assembly">Assembly for add type identifiers</param>
        void RegisterAssembly(Assembly assembly);
    }
}
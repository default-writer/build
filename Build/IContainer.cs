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
        /// <returns>Returns instance of identified type</returns>
        T CreateInstance<T>();
        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</typeparam>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(Type type);
        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="typeId">Type identifier with/without parameters 'typeid(args)' or 'typeid'</param>
        /// <param name="args">Argument list</param>
        /// <returns>Returns instance of identified type</returns>
        object CreateInstance(string typeId, params object[] args);
        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <returns>Returns true if type is supported and acually added to collection of identified types</returns>
        bool RegisterType<T>();  
        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <returns>Returns true if type is supported and acually added to collection of identified types</returns>
        bool Register(Type type);
        /// <summary>
        /// Registers all supported types in assembly
        /// </summary>
        /// <param name="assembly">Assembly for add type identifiers</param>
        void RegisterAssembly(Assembly assembly);
    }
}

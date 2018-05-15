using System;
using System.Reflection;

namespace Build
{
    /// <summary>
    /// Public class for type container
    /// </summary>
    public class Container : IContainer
    {
        readonly TypeBuilder _typeBuilder;

        public Container() => _typeBuilder = new TypeBuilder();

        public Container(ITypeFilter typeFilter, ITypeResolver typeResolver, ITypeParser typeParser) => _typeBuilder = new TypeBuilder(typeFilter, typeResolver, typeParser);

        public ITypeFilter TypeFilter { get; }

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public T CreateInstance<T>(params object[] args)
        {
            if (!_typeBuilder.CanCreate(typeof(T)))
                throw new TypeFilterException(string.Format("{0} is not instantiable (not an allowed type)", typeof(T).FullName));
            return (T)_typeBuilder.CreateInstance(typeof(T), args);
        }

        /// <summary> Creates an object </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param> <returns>Returns instance of
        /// identified type</returns>
        public object CreateInstance(Type type, params object[] args)
        {
            if (!_typeBuilder.CanCreate(type))
                throw new TypeFilterException(string.Format("{0} is not instantiable (not an allowed type)", type.FullName));
            return _typeBuilder.CreateInstance(type, args);
        }

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="id">Type identifier with/without parameters 'id(args)' or 'id'</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(string id, params object[] args)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            return _typeBuilder.CreateInstance(id, args);
        }

        /// <summary>
        /// Registers all supported types in assembly
        /// </summary>
        /// <param name="assembly">Assembly for add type identifiers</param>
        public void RegisterAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
                if (_typeBuilder.CanRegister(type))
                    _typeBuilder.RegisterType(type);
        }

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <returns>
        /// Returns true if type is supported and acually added to collection of identified types
        /// </returns>
        public void RegisterType(Type type)
        {
            if (!_typeBuilder.CanRegister(type))
                throw new TypeFilterException(string.Format("{0} is not instantiable (not an allowed type)", type.FullName));
            _typeBuilder.RegisterType(type);
        }

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <returns>
        /// Returns true if type is supported and acually added to collection of identified types
        /// </returns>
        public void RegisterType<T>()
        {
            if (!_typeBuilder.CanRegister(typeof(T)))
                throw new TypeFilterException(string.Format("{0} is not instantiable (not an allowed type)", typeof(T).FullName));
            _typeBuilder.RegisterType(typeof(T));
        }
    }
}
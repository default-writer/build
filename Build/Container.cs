using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Build
{
    /// <summary>
    /// Public class for type container
    /// </summary>
    public class Container : IContainer
    {
        /// <summary>
        /// The type builder
        /// </summary>
        readonly TypeBuilder _typeBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        public Container() => _typeBuilder = new TypeBuilder();

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="typeFilter">The type filter.</param>
        /// <param name="typeResolver">The type resolver.</param>
        /// <param name="typeParser">The type parser.</param>
        public Container(ITypeFilter typeFilter, ITypeResolver typeResolver, ITypeParser typeParser) => _typeBuilder = new TypeBuilder(typeFilter, typeResolver, typeParser);

        /// <summary>
        /// Aliased types.
        /// </summary>
        public IEnumerable<string> RuntimeAliasedTypes => new List<string>(_typeBuilder.RuntimeAliasedTypes);

        /// <summary>
        /// Non-aliased types.
        /// </summary>
        public IEnumerable<string> RuntimeNonAliasedTypes => new List<string>(_typeBuilder.RuntimeNonAliasedTypes);

        /// <summary>
        /// Type aliases
        /// </summary>
        public IEnumerable<string> RuntimeTypeAliases => new List<string>(_typeBuilder.RuntimeTypeAliases);

        /// <summary>
        /// Runtime types.
        /// </summary>
        public IEnumerable<string> RuntimeTypes => new List<string>(_typeBuilder.RuntimeTypes);

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public T CreateInstance<T>(params object[] args)
        {
            if (!_typeBuilder.CanCreate(typeof(T)))
                throw new TypeInstantiationException(string.Format("{0} is not instantiable (not an allowed type)", typeof(T).FullName));
            return (T)_typeBuilder.CreateInstance(typeof(T), args);
        }

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(Type type, params object[] args)
        {
            if (!_typeBuilder.CanCreate(type))
                throw new TypeInstantiationException(string.Format("{0} is not instantiable (not an allowed type)", type.FullName));
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
                throw new TypeInstantiationException(string.Format("{0} is null (type name required)", nameof(id)));
            return _typeBuilder.CreateInstance(id, args);
        }

        /// <summary>
        /// Registers all supported types in assembly
        /// </summary>
        /// <param name="assembly">Assembly for add type identifiers</param>
        /// <param name="exclusionTypes">List of assembly types to ignore</param>
        public void RegisterAssembly(Assembly assembly, string[] exclusionTypes)
        {
            var exclusionList = new List<string>
            {
                "<PrivateImplementationDetails>"
            };
            if (exclusionTypes != null)
                exclusionList.AddRange(exclusionTypes);
            foreach (var type in assembly.GetTypes())
            {
                if (exclusionList.Contains(type.FullName))
                    continue;
                if (_typeBuilder.CanRegister(type))
                    _typeBuilder.RegisterType(type);
            }
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

        /// <summary>
        /// Resets information about type registration
        /// </summary>
        public void Reset() => _typeBuilder.Reset();
    }
}

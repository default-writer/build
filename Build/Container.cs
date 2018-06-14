using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

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
        public Container() => _typeBuilder = new TypeBuilder(true, true);

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="typeConstructor">Type constructor</param>
        /// <param name="typeFilter">Type filter</param>
        /// <param name="typeParser">Type parser</param>
        /// <param name="typeResolver">Type resolver</param>
        /// <param name="defaultTypeResolution">
        /// Parameter defaults to true for automatic type resolution enabled. If value is false and
        /// not all type dependencies are resolved, exception will be thrown
        /// </param>
        /// <param name="defaultTypeInstantiation">
        /// Parameter defaults to true for automatic type instantiation enabled. If value is false
        /// and type is resolved to default value for reference type, exception will be thrown
        /// </param>
        public Container(ITypeConstructor typeConstructor, ITypeFilter typeFilter, ITypeParser typeParser, ITypeResolver typeResolver, bool defaultTypeResolution, bool defaultTypeInstantiation) => _typeBuilder = new TypeBuilder(typeConstructor, typeFilter, typeParser, typeResolver, defaultTypeResolution, defaultTypeInstantiation);

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="typeConstructor">Type constructor</param>
        /// <param name="typeFilter">Type filter</param>
        /// <param name="typeParser">Type parser</param>
        /// <param name="typeResolver">Type resolver</param>
        public Container(ITypeConstructor typeConstructor, ITypeFilter typeFilter, ITypeParser typeParser, ITypeResolver typeResolver) => _typeBuilder = new TypeBuilder(typeConstructor, typeFilter, typeParser, typeResolver, true, true);

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="defaultTypeResolution">
        /// Parameter defaults to true for automatic type resolution enabled. If value is false and
        /// not all type dependencies are resolved, exception will be thrown
        /// </param>
        /// <param name="defaultTypeInstantiation">
        /// Parameter defaults to true for automatic type instantiation enabled. If value is false
        /// and type is resolved to default value for reference type, exception will be thrown
        /// </param>
        public Container(bool defaultTypeResolution, bool defaultTypeInstantiation) => _typeBuilder = new TypeBuilder(defaultTypeResolution, defaultTypeInstantiation);

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
        /// Type builder
        /// </summary>
        public ITypeBuilder Builder => _typeBuilder;

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
        public void RegisterAssembly(Assembly assembly, IEnumerable<string> exclusionTypes)
        {
            var exclusionList = new List<string>(exclusionTypes ?? Array.Empty<string>())
            {
                "<PrivateImplementationDetails>"
            };
            bool match;
            foreach (var type in assembly.GetTypes())
            {
                match = false;
                for (int index = 0; index < exclusionList.Count; index++)
                {
                    var regex = string.Format("^{0}$", exclusionList[index]);
                    if (Regex.IsMatch(type.FullName, regex))
                    {
                        match = true;
                        break;
                    }
                }
                if (match)
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
                throw new TypeRegistrationException(string.Format("{0} is not instantiable (not an allowed type)", type.FullName));
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
                throw new TypeRegistrationException(string.Format("{0} is not instantiable (not an allowed type)", typeof(T).FullName));
            _typeBuilder.RegisterType(typeof(T));
        }

        /// <summary>
        /// Resets information about type registration
        /// </summary>
        public void Reset() => _typeBuilder.Reset();
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Build
{
    /// <summary>
    /// Public class for type container
    /// </summary>
    public sealed class Container : IContainer
    {
        /// <summary>
        /// The type builder
        /// </summary>
        readonly TypeBuilder _typeBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        public Container() => _typeBuilder = new TypeBuilder(new TypeBuilderOptions());

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="typeBuilderOptions">Type options</param>
        public Container(TypeBuilderOptions typeBuilderOptions) =>
            _typeBuilder = new TypeBuilder(typeBuilderOptions);

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="typeConstructor">Type constructor</param>
        /// <param name="typeFilter">Type filter</param>
        /// <param name="typeParser">Type parser</param>
        /// <param name="typeResolver">Type resolver</param>
        public Container(ITypeActivator typeActivator, ITypeConstructor typeConstructor, ITypeFilter typeFilter, ITypeParser typeParser, ITypeResolver typeResolver) =>
            _typeBuilder = new TypeBuilder(typeActivator, typeConstructor, typeFilter, typeParser, typeResolver);

        /// <summary>
        /// Type builder
        /// </summary>
        public ITypeBuilder Builder => _typeBuilder;

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
        public T CreateInstance<T>(params object[] args) => (T)CreateInstance(typeof(T), args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(Type type, params object[] args) => CreateInstance(type.ToString(), args);

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="typeFullName">Type identifier with/without parameters 'id(args)' or 'id'</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(string typeFullName, params object[] args)
        {
            if (typeFullName == null)
                throw new TypeInstantiationException(string.Format("{0} is null (type name required)", nameof(typeFullName)));
            return _typeBuilder.CreateInstance(typeFullName, args);
        }

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public T GetInstance<T>(params object[] args) => (T)GetInstance(typeof(T), args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(Type type, params object[] args) => GetInstance(type.ToString(), args);

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="typeFullName">Type identifier with/without parameters 'id(args)' or 'id'</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(string typeFullName, params object[] args)
        {
            if (typeFullName == null)
                throw new TypeInstantiationException(string.Format("{0} is null (type name required)", nameof(typeFullName)));
            return _typeBuilder.GetInstance(typeFullName, args);
        }

        /// <summary>
        /// Registers all supported types in assembly
        /// </summary>
        /// <param name="assembly">Assembly for add type identifiers</param>
        /// <param name="exclusionTypes">List of assembly types to ignore</param>
        public void RegisterAssembly(Assembly assembly, IEnumerable<string> exclusionTypes)
        {
            #region Target Frameworks

#if NET45 || NET451 || NET452
            var emptyArray = new string[0];
#else
            var emptyArray = Array.Empty<string>();
#endif

            #endregion

            var exclusionList = new List<string>(exclusionTypes ?? emptyArray)
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
                    if (Regex.IsMatch(type.ToString(), regex))
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
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Constructor arguments</param>
        public void RegisterType<T>(params object[] args) => RegisterType(typeof(T), args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        public void RegisterType(Type type, params object[] args)
        {
            if (!_typeBuilder.CanRegister(type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not an allowed type)", type));
            _typeBuilder.RegisterType(type, args);
        }

        /// <summary>
        /// Resets information about type registration
        /// </summary>
        public void Reset() => _typeBuilder.Reset();
    }
}
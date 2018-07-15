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
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        public Container() => TypeBuilder = new TypeBuilder(new TypeBuilderOptions());

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="typeBuilderOptions">Type options</param>
        public Container(TypeBuilderOptions typeBuilderOptions) => TypeBuilder = new TypeBuilder(typeBuilderOptions);

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="typeConstructor">Type constructor</param>
        /// <param name="typeFilter">Type filter</param>
        /// <param name="typeParser">Type parser</param>
        /// <param name="typeResolver">Type resolver</param>
        public Container(ITypeActivator typeActivator, ITypeConstructor typeConstructor, ITypeFilter typeFilter, ITypeParser typeParser, ITypeResolver typeResolver) => TypeBuilder = new TypeBuilder(typeActivator, typeConstructor, typeFilter, typeParser, typeResolver);

        /// <summary>
        /// Type builder
        /// </summary>
        public ITypeBuilder Builder => TypeBuilder;

        /// <summary>
        /// True if container is locked
        /// </summary>
        public bool IsLocked => TypeBuilder.IsLocked;

        /// <summary>
        /// Aliased types.
        /// </summary>
        public IEnumerable<string> RuntimeAliasedTypes => new List<string>(TypeBuilder.RuntimeAliasedTypes);

        /// <summary>
        /// Non-aliased types.
        /// </summary>
        public IEnumerable<string> RuntimeNonAliasedTypes => new List<string>(TypeBuilder.RuntimeNonAliasedTypes);

        /// <summary>
        /// Type aliases
        /// </summary>
        public IEnumerable<string> RuntimeTypeAliases => new List<string>(TypeBuilder.RuntimeTypeAliases);

        /// <summary>
        /// Runtime types.
        /// </summary>
        public IEnumerable<string> RuntimeTypes => new List<string>(TypeBuilder.RuntimeTypes);

        /// <summary>
        /// Type builder reference
        /// </summary>
        public TypeBuilder TypeBuilder { get; private set; }

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <returns>Returns instance of identified type</returns>
        public T CreateInstance<T>(params object[] args) => (T)TypeBuilder.CreateInstance(typeof(T), args);

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <returns>Returns instance of identified type</returns>
        public T CreateInstance<T>() => (T)TypeBuilder.CreateInstance(typeof(T), Array.Empty<object>());

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(Type type, params object[] args) => TypeBuilder.CreateInstance(type, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(Type type, params string[] args) => TypeBuilder.CreateInstance(type, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(Type type, params Type[] args) => TypeBuilder.CreateInstance(type, args);

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="typeFullName">Type identifier with/without parameters 'id(args)' or 'id'</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(string typeFullName, params object[] args) => TypeBuilder.CreateInstance(typeFullName, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(string typeId, params string[] args) => TypeBuilder.CreateInstance(typeId, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(string typeId, params Type[] args) => TypeBuilder.CreateInstance(typeId, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(Type type) => TypeBuilder.CreateInstance(type, Array.Empty<object>());

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public T CreateInstance<T>(params string[] args) => (T)TypeBuilder.CreateInstance(typeof(T), args);

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public T CreateInstance<T>(params Type[] args) => (T)TypeBuilder.CreateInstance(typeof(T), args);

        /// <summary>
        /// Creates an object from identifed type with parameters
        /// </summary>
        /// <param name="typeFullName">Type identifier with/without parameters 'id(args)' or 'id'</param>
        /// <returns>Returns instance of identified type</returns>
        public object CreateInstance(string typeFullName) => TypeBuilder.CreateInstance(typeFullName, Array.Empty<object>());

        /// <summary>
        /// Creates an object identified as instance of type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public T GetInstance<T>(params object[] args) => (T)TypeBuilder.GetInstance(typeof(T), args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeFullName">Type identifier</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(string typeFullName) => TypeBuilder.GetInstance(typeFullName, Array.Empty<object>());

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeFullName">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(string typeFullName, params string[] args) => TypeBuilder.GetInstance(typeFullName, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeFullName">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(string typeFullName, params object[] args) => TypeBuilder.GetInstance(typeFullName, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="typeFullName">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(string typeFullName, params Type[] args) => TypeBuilder.GetInstance(typeFullName, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(Type type, params Type[] args) => TypeBuilder.GetInstance(type, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(Type type) => TypeBuilder.GetInstance(type, Array.Empty<object>());

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(Type type, params string[] args) => TypeBuilder.GetInstance(type, args);

        /// <summary>
        /// Creates an object
        /// </summary>
        /// <param name="type">Type identifier</param>
        /// <param name="args">Arguments to constuctor</param>
        /// <returns>Returns instance of identified type</returns>
        public object GetInstance(Type type, params object[] args) => TypeBuilder.GetInstance(type, args);

        /// <summary>
        /// Locks the container. Pre-computes all registered type invariants for lookup table speed up
        /// </summary>
        public void Lock() => TypeBuilder.Lock();

        /// <summary>
        /// Registers all supported types in assembly
        /// </summary>
        /// <param name="assembly">Assembly for add type identifiers</param>
        /// <param name="exclusionTypes">List of assembly types to ignore</param>
        public void RegisterAssembly(Assembly assembly, IEnumerable<string> exclusionTypes)
        {
            if (TypeBuilder.IsLocked)
                throw new TypeRegistrationException(string.Format("{0} is not registered (container locked)", nameof(assembly)));
            var exclusionList = new List<string>(exclusionTypes ?? Array.Empty<string>()) { "<PrivateImplementationDetails>" };
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
                if (TypeBuilder.CanRegister(type))
                    TypeBuilder.RegisterType(type);
            }
        }

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Constructor arguments</param>
        public void RegisterType<T>(params object[] args) => TypeBuilder.RegisterType(typeof(T), TypeBuilder.IsNotNull(args));

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Constructor arguments</param>
        public void RegisterType<T>(params string[] args) => TypeBuilder.RegisterType(typeof(T), TypeBuilder.IsNotNull(args));

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        /// <param name="args">Constructor arguments</param>
        public void RegisterType<T>(params Type[] args) => TypeBuilder.RegisterType(typeof(T), args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        public void RegisterType(Type type, params object[] args) => TypeBuilder.RegisterType(type, args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        public void RegisterType(Type type) => TypeBuilder.RegisterType(type, Array.Empty<object>());

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        public void RegisterType(string typeId) => TypeBuilder.RegisterType(typeId, Array.Empty<object>());

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        public void RegisterType(string typeId, params object[] args) => TypeBuilder.RegisterType(typeId, args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        public void RegisterType(string typeId, params string[] args) => TypeBuilder.RegisterType(typeId, args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="typeId">Type identifier</param>
        public void RegisterType(string typeId, params Type[] args) => TypeBuilder.RegisterType(typeId, args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="type">Type identifier</param>
        public void RegisterType(Type type, params string[] args) => TypeBuilder.RegisterType(type, args);

        /// <summary>
        /// Regeters type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="args">The args.</param>
        public void RegisterType(Type type, params Type[] args) => TypeBuilder.RegisterType(type, args);

        /// <summary>
        /// Registers identified type T
        /// </summary>
        /// <typeparam name="T">Type identifier</typeparam>
        public void RegisterType<T>() => TypeBuilder.RegisterType(typeof(T), Array.Empty<object>());

        /// <summary>
        /// Resets information about type registration. Also, resets freezed containers
        /// </summary>
        public void Reset() => TypeBuilder.Reset();

        /// <summary>
        /// Unlocks the container. Flushes all pre-computed registered type invariants for lookup table speed up
        /// </summary>
        public void Unlock() => TypeBuilder.Unlock();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    /// <summary>
    /// Type builder
    /// </summary>
    class TypeBuilder : ITypeBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBuilder"/> class.
        /// </summary>
        /// <param name="defaultTypeResolution">
        /// Parameter defaults to true for automatic type resolution enabled. If value is false and
        /// not all type dependencies are resolved, exception will be thrown
        /// </param>
        /// <param name="defaultTypeInstantiation">
        /// Parameter defaults to true for automatic type instantiation enabled. If value is false
        /// and type is resolved to default value for reference type, exception will be thrown
        /// </param>
        public TypeBuilder(bool defaultTypeResolution, bool defaultTypeInstantiation)
        {
            DefaultTypeResolution = defaultTypeResolution;
            DefaultTypeInstantiation = defaultTypeInstantiation;
            Constructor = new TypeConstructor();
            Filter = new TypeFilter();
            Resolver = new TypeResolver();
            Parser = new TypeParser();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBuilder"/> class.
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
        public TypeBuilder(ITypeConstructor typeConstructor, ITypeFilter typeFilter, ITypeParser typeParser, ITypeResolver typeResolver, bool defaultTypeResolution, bool defaultTypeInstantiation)
        {
            DefaultTypeResolution = defaultTypeResolution;
            DefaultTypeInstantiation = defaultTypeInstantiation;
            Constructor = typeConstructor ?? throw new ArgumentNullException(nameof(typeConstructor));
            Filter = typeFilter ?? throw new ArgumentNullException(nameof(typeFilter));
            Resolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
            Parser = typeParser ?? throw new ArgumentNullException(nameof(typeParser));
        }

        /// <summary>
        /// True if automatic type resolution for reference types option enabled (does not throws
        /// exceptions for reference types contains type dependencies to non-registered types)
        /// </summary>
        /// <remarks>
        /// If automatic type resolution for reference types is enabled, type will defaults to null
        /// if not resolved and no exception will be thrown
        /// </remarks>
        public bool DefaultTypeResolution { get; }

        /// <summary>
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        public bool DefaultTypeInstantiation { get; }

        /// <summary>
        /// Gets the runtime aliased types.
        /// </summary>
        /// <value>The type aliases.</value>
        public IEnumerable<string> RuntimeAliasedTypes => Types.Where(p => p.Key != p.Value.Id).Select(p => p.Value.Id);

        /// <summary>
        /// Gets the runtime non aliased types.
        /// </summary>
        /// <value>The runtime non aliased types.</value>
        public IEnumerable<string> RuntimeNonAliasedTypes => Types.Where(p => p.Key == p.Value.Id).Select(p => p.Value.Id);

        /// <summary>
        /// Gets the runtime aliases.
        /// </summary>
        /// <value>The runtime aliases.</value>
        public IEnumerable<string> RuntimeTypeAliases => Types.Where(p => p.Key != p.Value.Id).Select(p => p.Key);

        /// <summary>
        /// Gets the runtime types.
        /// </summary>
        /// <value>The runtime types.</value>
        public IEnumerable<string> RuntimeTypes => Types.Select(p => p.Value.Id);

        /// <summary>
        /// Constructs type dependency
        /// </summary>
        public ITypeConstructor Constructor { get; }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public ITypeFilter Filter { get; }

        /// <summary>
        /// Gets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public ITypeParser Parser { get; }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>The resolver.</value>
        public ITypeResolver Resolver { get; }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <value>The types.</value>
        IDictionary<string, RuntimeType> Types { get; } = new Dictionary<string, RuntimeType>();

        /// <summary>
        /// Gets the visited.
        /// </summary>
        /// <value>The visited.</value>
        List<Type> Visited { get; } = new List<Type>();

        /// <summary>
        /// Gets the <see cref="RuntimeType"/> with the specified identifier.
        /// </summary>
        /// <value>The <see cref="RuntimeType"/>.</value>
        /// <param name="id">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        RuntimeType this[string id, RuntimeType type]
        {
            get
            {
                if (!Types.ContainsKey(id))
                    Types.Add(id, type);
                Types[id].RegisterAssignableType(type.Type);
                return Types[id];
            }
        }

        /// <summary>
        /// Determines whether this instance can create the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can create the specified type; otherwise, <c>false</c>.</returns>
        public bool CanCreate(Type type) => Filter.CanCreate(type);

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRegister(Type type) => Filter.CanRegister(type);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public object CreateInstance(Type type, params object[] args) => CreateInstance(type.FullName, args);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateInstance(string id, params object[] args)
        {
            if (Types.ContainsKey(id))
                return Types[id].CreateInstance(args);
            var parameterArgs = Format.GetParametersFullName(args);
            var runtimeType = (RuntimeType)Parser.Find(id, parameterArgs, Types.Values);
            if (runtimeType != null) return runtimeType.CreateInstance(args);
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructors available)", id));
        }

        /// <summary>
        /// Registers the type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void RegisterType(Type type)
        {
            Visited.Add(type);
            try
            {
                RegisterConstructor(type);
            }
            catch (TypeRegistrationException ex)
            {
                throw new TypeRegistrationException(string.Format("{0} is not registered", type.FullName), ex);
            }
            finally
            {
                Visited.Remove(type);
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        internal void Reset() => Types.Clear();

        /// <summary>
        /// Checks the full name of the parameter type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="id">The identifier.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        static void CheckParameterTypeFullName(Type type, Type parameterType, string id)
        {
            if (id == type.FullName && id == parameterType.FullName)
                throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
        }

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns></returns>
        /// <exception cref="TypeRegistrationException"></exception>
        static void CheckParametersFullName(Type type, Type parameterType, string id, Type attributeType)
        {
            if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
            CheckParameterTypeFullName(type, parameterType, id);
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="typeFullName">The type full name.</param>
        /// <returns></returns>
        /// <exception cref="TypeRegistrationException"></exception>
        string GetAssemblyTypeFullName(Type type, string typeFullName)
        {
            string id = typeFullName ?? type.FullName;
            var attributeType = Resolver.GetType(type.Assembly, id);
            if (attributeType != null && !attributeType.IsAssignableFrom(type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
            return id;
        }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="constructorParametersFullNames">The constructor parameters full names.</param>
        /// <param name="typeFullName">The type full name.</param>
        /// <returns></returns>
        string GetTypeFullName(Type type, IEnumerable<string> constructorParametersFullNames, string typeFullName)
        {
            string id = GetAssemblyTypeFullName(type, typeFullName);
            var parameterArgs = constructorParametersFullNames;
            var runtimeType = (RuntimeType)Parser.Find(id, parameterArgs, Types.Values);
            string constructorRuntimeFullName = runtimeType == null ? id : runtimeType.Type.FullName;
            return Format.GetConstructorFullName(constructorRuntimeFullName, parameterArgs);
        }

        /// <summary>
        /// Registers the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        void RegisterConstructor(Type type)
        {
            var constructorEnumerator = Constructor.GetDependencyObjects(type, DefaultTypeInstantiation).GetEnumerator();
            if (!constructorEnumerator.MoveNext())
                throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", type.FullName));
            do
            {
                var dependencyObject = constructorEnumerator.Current;
                foreach (var injectionObject in dependencyObject.InjectionObjects)
                {
                    RegisterConstructorParameter(dependencyObject, injectionObject);
                }
                RegisterConstructorDependencyObject(dependencyObject);
            } while (constructorEnumerator.MoveNext());
        }

        /// <summary>
        /// Registers the constructor parameter.
        /// </summary>
        /// <param name="dependencyObject">The constructor.</param>
        /// <param name="injectionObject">The constructor parameter.</param>
        void RegisterConstructorParameter(ITypeDependencyObject dependencyObject, ITypeInjectionObject injectionObject)
        {
            var constructor = dependencyObject.RuntimeType;
            var type = constructor.Type;
            var parameter = injectionObject.RuntimeType;
            string id = injectionObject.InjectionAttribute.TypeFullName ?? parameter.Type.FullName;
            var attributeType = Resolver.GetType(type.Assembly, id);
            CheckParametersFullName(type, parameter.Type, id, attributeType);
            var parameters = injectionObject.InjectionAttribute.GetParametersFullName();
            var runtimeType = (RuntimeType)Parser.Find(id, parameters, Types.Values);
            if (DefaultTypeResolution && runtimeType == null)
                RegisterConstructorType(attributeType);
            RegisterConstructorType(parameter.Type);
            string constructorRuntimeTypeFullName = runtimeType == null ? id : runtimeType.Type.FullName;
            RegisterRuntimeType(dependencyObject, injectionObject, constructor, parameter, parameters, constructorRuntimeTypeFullName);
        }

        void RegisterRuntimeType(ITypeDependencyObject dependencyObject, ITypeInjectionObject injectionObject, RuntimeType constructor, RuntimeType parameter, IEnumerable<string> parameters, string constructorRuntimeTypeFullName)
        {
            var type = constructor.Type;
            var constructorParameters = dependencyObject.InjectionObjectsFullNames;
            var typeFullName = Format.GetConstructorFullName(constructorRuntimeTypeFullName, parameters);
            var result = this[typeFullName, parameter];
            if (result != null)
            {
                var constructorFullName = Format.GetConstructorFullName(type.FullName, constructorParameters);
                result.Attribute.RegisterRuntimeType(constructorFullName, injectionObject.InjectionAttribute);
                constructor.AddParameter(result);
            }
        }

        /// <summary>
        /// Registers the type of the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        void RegisterConstructorType(Type type)
        {
            if (Filter.CanRegister(type))
            {
                if (Visited.Contains(type))
                    throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
                RegisterType(type);
            }
        }

        /// <summary>
        /// Registers the type of the constructor.
        /// </summary>
        /// <param name="dependencyObject">Type dependency object.</param>
        void RegisterConstructorDependencyObject(ITypeDependencyObject dependencyObject)
        {
            var constructor = dependencyObject.RuntimeType;
            var attribute = dependencyObject.DependencyAttribute;
            var constructorType = dependencyObject.RuntimeType.Type;
            var constructorParametersFullNames = dependencyObject.InjectionObjectsFullNames;
            string typeFullName = GetTypeFullName(constructorType, constructorParametersFullNames, attribute.TypeFullName);
            if (!Types.ContainsKey(typeFullName))
            {
                var result = this[typeFullName, constructor];
                if (result != null)
                {
                    result.Initialize(attribute.RuntimeInstance);
                }
            }
        }
    }
}
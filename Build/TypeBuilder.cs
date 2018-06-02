using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection;

namespace Build
{
    /// <summary>
    /// Type builder
    /// </summary>
    class TypeBuilder
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
            Filter = new TypeFilter();
            Resolver = new TypeResolver();
            Parser = new TypeParser();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBuilder"/> class.
        /// </summary>
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
        public TypeBuilder(ITypeFilter typeFilter, ITypeResolver typeResolver, ITypeParser typeParser, bool defaultTypeResolution, bool defaultTypeInstantiation)
        {
            DefaultTypeResolution = defaultTypeResolution;
            DefaultTypeInstantiation = defaultTypeInstantiation;
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
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        ITypeFilter Filter { get; }

        /// <summary>
        /// Gets the parser.
        /// </summary>
        /// <value>The parser.</value>
        ITypeParser Parser { get; }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>The resolver.</value>
        ITypeResolver Resolver { get; }

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
            var parameterArgs = GetParametersFullName(args);
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
        /// Gets the dependency attribute.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        /// <returns></returns>
        static DependencyAttribute GetDependencyAttribute(ConstructorInfo constructor) => constructor.GetCustomAttribute<DependencyAttribute>() ?? new DependencyAttribute(constructor.DeclaringType, RuntimeInstance.CreateInstance);

        /// <summary>
        /// Gets the injection attribute.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        static InjectionAttribute GetInjectionAttribute(ParameterInfo parameter) => parameter.GetCustomAttribute<InjectionAttribute>() ?? new InjectionAttribute(parameter.ParameterType);

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="injectionAttribute">The injection attribute.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns></returns>
        /// <exception cref="TypeRegistrationException"></exception>
        static IEnumerable<string> GetParametersFullName(Type type, Type parameterType, InjectionAttribute injectionAttribute, string id, Type attributeType)
        {
            if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
            CheckParameterTypeFullName(type, parameterType, id);
            return GetParametersFullName(injectionAttribute.Arguments);
        }

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        static IEnumerable<string> GetParametersFullName(object[] args) => args == null ? Array.Empty<string>() : args.Select(p => (p ?? typeof(object)).GetType().FullName).ToArray();

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        /// <exception cref="TypeRegistrationException"></exception>
        string GetId(Type type, IRuntimeAttribute attribute)
        {
            string id = Resolver.GetTypeFullName(attribute, type.FullName);
            var attributeType = Resolver.GetType(type.Assembly, id);
            if (attributeType != null && !attributeType.IsAssignableFrom(type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
            return id;
        }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="constructorParameters">The constructor parameters.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        string GetTypeFullName(Type type, ParameterInfo[] constructorParameters, IRuntimeAttribute attribute)
        {
            string id = GetId(type, attribute);
            var parameterArgs = constructorParameters.Select(p => p.ParameterType.FullName);
            var runtimeType = (RuntimeType)Parser.Find(id, parameterArgs, Types.Values);
            string constructorRuntimeFullName = runtimeType == null ? id : runtimeType.Type.FullName;
            string typeFullName = Format.GetConstructorFullName(constructorRuntimeFullName, parameterArgs);
            return typeFullName;
        }

        /// <summary>
        /// Registers the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        void RegisterConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", type.FullName));
            foreach (var constructorInfo in constructors)
            {
                var constructorParameters = constructorInfo.GetParameters();
                var dependencyAttribute = GetDependencyAttribute(constructorInfo);
#if PARENT_STRATEGY
                var constructor = new RuntimeType(dependencyAttribute, null, type);
#else
                var constructor = new RuntimeType(dependencyAttribute, type, DefaultTypeInstantiation);
#endif
                for (int i = 0; i < constructorParameters.Length; i++)
                {
                    RegisterConstructorParameter(i, type, constructor, constructorParameters);
                }
                var attribute = constructor.Attribute;
                string typeFullName = GetTypeFullName(type, constructorParameters, attribute);
                RegisterConstructorType(typeFullName, constructor, attribute);
            }
        }

        /// <summary>
        /// Registers the constructor parameter.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="type">The type.</param>
        /// <param name="constructor">The constructor.</param>
        /// <param name="constructorParameters">The parameter array.</param>
        void RegisterConstructorParameter(int i, Type type, RuntimeType constructor, ParameterInfo[] constructorParameters)
        {
            var parameterType = constructorParameters[i].ParameterType;
            var injectionAttribute = GetInjectionAttribute(constructorParameters[i]);
#if PARENT_STRATEGY
            var parameter = new RuntimeType(injectionAttribute, constructor, constructorParameters[i].ParameterType);
#else
            var parameter = new RuntimeType(injectionAttribute, constructorParameters[i].ParameterType, DefaultTypeInstantiation);
#endif
            string id = Resolver.GetTypeFullName(injectionAttribute, parameterType.FullName);
            var attributeType = Resolver.GetType(type.Assembly, id);
            var parameters = GetParametersFullName(type, parameterType, injectionAttribute, id, attributeType);
            var runtimeType = (RuntimeType)Parser.Find(id, parameters, Types.Values);
            if (DefaultTypeResolution && runtimeType == null)
                RegisterConstructorType(attributeType);
            RegisterConstructorType(parameterType);
            string constructorRuntimeTypeFullName = runtimeType == null ? id : runtimeType.Type.FullName;
            string typeFullName = Format.GetConstructorFullName(constructorRuntimeTypeFullName, parameters);
            var result = this[typeFullName, parameter];
            if (result != null)
            {
                var constructorFullName = Format.GetConstructorFullName(type.FullName, constructorParameters.Select(p => p.ParameterType.FullName));
                var parameterId = Format.GetConstructorParameterFullName(constructorFullName, i);
                result.Attribute.RegisterRuntimeType(parameterId, injectionAttribute);
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
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="constructor">The constructor.</param>
        /// <param name="attribute">The runtime attribute.</param>
        void RegisterConstructorType(string typeFullName, RuntimeType constructor, IRuntimeAttribute attribute)
        {
#if PARENT_STRATEGY
            if (Types.ContainsKey(typeFullName) && this[typeFullName, Types[typeFullName]].IsInitialized)
            {
                return;
            }
#endif
            if (!Types.ContainsKey(typeFullName))
            {
                var result = this[typeFullName, constructor];
                if (result != null)
                {
                    result.Initialize(attribute.RuntimeInstance);
#if DEBUG
                    Debug.WriteLine("{0} set to {1}", result, attribute.RuntimeInstance);
#endif
                }
            }
        }
    }
}